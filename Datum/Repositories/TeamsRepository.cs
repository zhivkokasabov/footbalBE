using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datum.Repositories
{
    public class TeamsRepository : Repository<Team>, ITeamsRepository
    {
        public TeamsRepository(FootballManagerDbContext context): base(context)
        {

        }

        public async Task<Team> FindAsync(string entryKey)
        {
            return await DbContext.Teams
                .Include(t => t.Members)
                .FirstOrDefaultAsync(t => t.EntryKey == entryKey);
        }

        public async Task<Team> GetTeam(int teamId)
        {
            return (await DbContext.Teams
                .Where(t => t.TeamId == teamId)
                .Select(t => new Team
                {
                    TeamId = t.TeamId,
                    Name = t.Name,
                    Members = t.Members
                        .Where(m => m.Active)
                        .Select(m => new User
                        {
                            Nickname = m.Nickname,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            IsTeamCaptain = m.IsTeamCaptain,
                            Id = m.Id,
                            UserPositions = m.UserPositions
                                .Where(up => up.Active)
                                .Select(up => new UserPosition
                                {
                                    PlayerPosition = up.PlayerPosition
                                }).ToList()
                        }).ToList()
                })
                .ToListAsync()).First();
        }

        public async Task<List<TournamentMatch>> GetTeamMatches(int teamId)
        {
            return await DbContext.TournamentMatches
                .Where(x => x.TournamentMatchTeams.Any(tmt => tmt.TeamId == teamId))
                .Select(x => new TournamentMatch {
                    AwayTeamSequenceId = x.AwayTeamSequenceId,
                    HomeTeamSequenceId = x.HomeTeamSequenceId,
                    Round = x.Round,
                    SequenceId = x.SequenceId,
                    StartTime = x.StartTime,
                    TournamentMatchTeams = x.TournamentMatchTeams.Any() ?
                        x.TournamentMatchTeams.Select(tmt => new TournamentMatchTeam
                        {
                            IsHomeTeam = tmt.IsHomeTeam,
                            Team = new Team
                            {
                                Name = tmt.Team.Name
                            }
                        }).ToList() : new List<TournamentMatchTeam>()
                })
                .ToListAsync();
        }

        public async Task<Team> GetUserTeam(int userId)
        {
            return await DbContext.Teams
                .FirstOrDefaultAsync(x => x.Members.Any(u => u.Id == userId));
        }
    }
}
