using AutoMapper;
using Core.Contracts.Request.Tournaments;
using Core.Contracts.Response.Tournaments;
using Core.Enums;
using Core.InternalObjects;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using Services.TournamentMatches.Factories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.TournamentMatches
{
    public class TournamentMatchesService: ITournamentMatchesService
    {
        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }

        public TournamentMatchesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<UpsertTournamentMatchModel> UpsertTournamentMatch(MatchResultDto result, int matchId)
        {
            var tournament = await UnitOfWork.Tournaments
                .GetTournamentQueryable()
                .Where(x => x.TournamentMatches.Any(tm => tm.TournamentMatchId == matchId))
                .Select(x => new Tournament
                {
                    TournamentId = x.TournamentId,
                    TournamentTypeId = x.TournamentTypeId,
                    TournamentMatches = x.TournamentMatches.Select(tm => new TournamentMatch {
                        TournamentId = tm.TournamentId,
                        AwayTeamSequenceId = tm.AwayTeamSequenceId,
                        HomeTeamSequenceId = tm.HomeTeamSequenceId,
                        IsEliminationMatch = tm.IsEliminationMatch,
                        Result = tm.Result,
                        Round = tm.Round,
                        SequenceId = tm.SequenceId,
                        StartTime = tm.StartTime,
                        TournamentMatchId = tm.TournamentMatchId,
                        TournamentMatchTeams = tm.TournamentMatchTeams.Where(tmt => tmt.Active)
                            .Select(tmt => new TournamentMatchTeam
                            {
                                Active = tmt.Active,
                                IsHomeTeam = tmt.IsHomeTeam,
                                Team = tmt.Team,
                                TeamId = tmt.TeamId,
                                TournamentMatchId = tmt.TournamentMatchId,
                                TournamentMatchTeamId = tmt.TournamentMatchTeamId
                            })
                            .ToList()
                    }).ToList(),
                    TournamentParticipants = x.TournamentParticipants.ToList()
                }).FirstOrDefaultAsync();

            var match = tournament.TournamentMatches.Find(x => x.TournamentMatchId == matchId);

            if (match.IsEliminationMatch && tournament.TournamentTypeId == (int)TournamentTypes.Classic)
            {
                tournament.TournamentTypeId = (int)TournamentTypes.Elimination;
            }

            var tournamentMatchBuilder = new MatchFactoryBuilder(
                tournament,
                result,
                matchId
                );

            if (tournamentMatchBuilder.Errors.Any())
            {
                return new UpsertTournamentMatchModel
                {
                    Errors = tournamentMatchBuilder.Errors
                };
            }

            tournamentMatchBuilder.UpdateParticipants();
            tournamentMatchBuilder.UpdateMatch();

            UnitOfWork.TournamentMatches.Update(tournamentMatchBuilder.Matches);
            UnitOfWork.TournamentParticipants.Update(tournament.TournamentParticipants);

            await UnitOfWork.CommitAsync();

            var matchesOutput = Mapper.Map<List<TournamentMatchOutputDto>>(tournamentMatchBuilder.Matches);
            var groupedMatches = TournamentMatchHelper.GroupMatches(TournamentMatchHelper.UpdateMatchesCanEdit(matchesOutput, tournament));

            return new UpsertTournamentMatchModel
            {
                Matches = groupedMatches
            };
        }
    }
}
