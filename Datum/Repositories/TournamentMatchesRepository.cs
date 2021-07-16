using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Repositories
{
    public class TournamentMatchesRepository: Repository<TournamentMatch>, ITournamentMatchesRepository
    {
        public TournamentMatchesRepository(FootballManagerDbContext context): base(context)
        {

        }

        public async Task<List<TournamentMatch>> GetEliminationTournamentMatches(int tournamentId)
        {
            return await DbContext.TournamentMatches
                .Where(x => x.TournamentId == tournamentId && x.IsEliminationMatch)
                .ToListAsync();
        }

        public async Task<List<TournamentMatch>> GetTournamentMatches(int tournamentId)
        {
            return await DbContext.TournamentMatches
                .Where(x => x.TournamentId == tournamentId)
                .Select(x => new TournamentMatch
                {
                    TournamentMatchId = x.TournamentMatchId,
                    AwayTeamSequenceId = x.AwayTeamSequenceId,
                    HomeTeamSequenceId = x.HomeTeamSequenceId,
                    Round = x.Round,
                    SequenceId = x.SequenceId,
                    StartTime = x.StartTime,
                    Result = x.Result,
                    IsEliminationMatch = x.IsEliminationMatch,
                    TournamentMatchTeams = x.TournamentMatchTeams.Any() ?
                        x.TournamentMatchTeams
                            .Where(tmt => tmt.Active)
                            .Select(tmt => new TournamentMatchTeam
                            {
                                IsHomeTeam = tmt.IsHomeTeam,
                                Team = new Team
                                {
                                    Name = tmt.Team.Name,
                                    TeamId = tmt.TeamId
                                }
                            }).ToList() : new List<TournamentMatchTeam>()
                })
                .ToListAsync();
        }

        public IQueryable<TournamentMatch> GetTournamentMatchesQueryable(int tournamentId)
        {
            return DbContext.TournamentMatches
                .Where(x => x.TournamentId == tournamentId);
        }

        public override void Update(List<TournamentMatch> entities)
        {
            entities.ForEach(x =>
            {
                x.TournamentMatchTeams.ForEach(y =>
                {
                    var teamId = y.TeamId;
                    var entry = DbContext.Entry(y);
                    entry.State = EntityState.Modified;
                    entry.Entity.TeamId = teamId;
                });
            });

            base.Update(entities);
        }
    }
}
