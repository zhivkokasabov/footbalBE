using AutoMapper;
using Core.contracts.response;
using Core.contracts.Response;
using Core.Contracts.Response.Tournaments;
using Core.Enums;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Services.Tournaments.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class TournamentService : ITournamentsService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public TournamentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<Tournament> CreateTournament(Tournament tournament, int userId)
        {
            var tournamentBuilder = new TournamentFactoryBuilder(tournament);

            tournament.UserId = userId;
            tournamentBuilder.CreateParticipants();
            tournamentBuilder.CreateTournamentMatches();

            await UnitOfWork.Tournaments.AddAsync(tournament);
            await UnitOfWork.CommitAsync();

            return tournament;
        }

        public async Task<List<Tournament>> GetUserTournaments(int userId, string role)
        {
            if (role == Roles.Organization)
            {
                return await UnitOfWork.Tournaments.GetUserTournaments(userId);
            }

            return await UnitOfWork.Tournaments.GetTournamentWithUserParticipation(userId);
        }

        public async Task<List<Tournament>> GetAllTournaments(int pageSize, int page)
        {
            return await UnitOfWork.Tournaments.GetAllTournamentsAsync(pageSize, page);
        }

        public async Task<Tournament> GetTournamentById(int tournamentId)
        {
            return await UnitOfWork.Tournaments.GetTournamentById(tournamentId);
        }

        public async Task<List<TournamentParticipant>> GetTournamentParticipants(int tournamentId)
        {
            return await UnitOfWork.TournamentParticipants.GetTournamentParticipants(tournamentId);
        }

        public async Task<IEnumerable<IGrouping<int, TournamentMatch>>> GetTournamentMatches(int tournamentId)
        {
            var matches = await UnitOfWork.TournamentMatches.GetTournamentMatches(tournamentId);
            var matchesOutput = Mapper.Map<List<TournamentMatchOutputDto>>(matches);

            return matches.GroupBy(x => x.Round).OrderBy(x => x.ElementAt(0).Round);
        }

        public async Task<List<ErrorModel>> JoinTournament(int tournamentId, int teamId)
        {
            var errors = new List<ErrorModel>();
            var team = await UnitOfWork.Teams.GetTeam(teamId);
            var tournament = await UnitOfWork.Tournaments.GetTournamentById(tournamentId);

            if (team == null)
            {
                errors.Add(new ErrorModel { Error = "Team does not exists" });
                return errors;
            }

            if (tournament.TournamentAccessId == (int)Core.Enums.TournamentAccess.Public)
            {
                var participant = await UnitOfWork.TournamentParticipants.FindUnassignedParticipant(tournamentId);

                if (participant == null)
                {
                    errors.Add(new ErrorModel { Error = "No available slots in the tournament" });
                    return errors;
                }

                var matches = await UnitOfWork.Tournaments.GetTournamentMatchesBySequenceId(participant.SequenceId, tournamentId);
                participant.TeamId = teamId;

                foreach (var match in matches)
                {
                    var isHomeTeam = match.HomeTeamSequenceId == participant.SequenceId;

                    match.TournamentMatchTeams.Add(
                        new TournamentMatchTeam { 
                            IsHomeTeam = isHomeTeam,
                            TeamId = team.TeamId,
                        });
                }

                await UnitOfWork.CommitAsync();
            }
            else
            {
                errors.Add(new ErrorModel { Error = "Cannot join non public tournament" });
                return errors;
            }

            return errors;
        }

        public async Task<bool> GetUserIsAllowedToParticipate(int tournamentId, int userId)
        {
            var user = await UnitOfWork.Users.GetUserAsync(userId);
            var tournamentParticipant = UnitOfWork.TournamentParticipants.Find((x) => x.TeamId == user.TeamId);

            return user.IsTeamCaptain && tournamentParticipant.Count() == 0;
        }
    }
}
