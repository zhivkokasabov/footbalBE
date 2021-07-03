using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datum.Repositories
{
    public class TournamentParticipantsRepository : Repository<TournamentParticipant>, ITournamentParticipantsRepository
    {
        public TournamentParticipantsRepository(FootballManagerDbContext context): base(context)
        {
        }

        public async Task<TournamentParticipant> FindUnassignedParticipant(int tournamentId)
        {
            return await DbContext.TournamentParticipants
                .FirstOrDefaultAsync(x => x.TeamId == null && x.TournamentId == tournamentId);
        }

        public async Task<TournamentParticipant> GetTournamentParticipant(int tournamentId, int teamId)
        {
            return await DbContext.TournamentParticipants
                .FirstOrDefaultAsync(x => x.TeamId == teamId && x.TournamentId == tournamentId);
        }

        public async Task<int> GetNumberOfEnrolledTeams(int tournamentId)
        {
            return await DbContext.TournamentParticipants
                .Where(x => x.TournamentId == tournamentId && x.TeamId != null)
                .CountAsync();
        }

        public async Task<List<TournamentParticipant>> GetTournamentParticipants(int tournamentId)
        {
            return await DbContext.TournamentParticipants
                    .Where(x => x.TournamentId == tournamentId)
                    .Select(x => new TournamentParticipant {
                        Group = x.Group,
                        SequenceId = x.SequenceId,
                        TeamId = x.TeamId,
                        TournamentId = x.TournamentId,
                        ConceivedGoals = x.ConceivedGoals,
                        Draws = x.Draws,
                        GoalDifference = x.GoalDifference,
                        Goals = x.Goals,
                        Loses = x.Loses,
                        Played = x.Played,
                        Points = x.Points,
                        Wins = x.Wins,
                        Team = x.TeamId.HasValue
                            ? new Team
                                {
                                    Name = x.Team.Name
                                }
                            : null
                    })
                    .ToListAsync();
        }
    }
}
