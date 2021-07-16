using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datum.Repositories
{
    public class TournamentRepository: Repository<Tournament>, ITournamentsRepository
    {
        public TournamentRepository(FootballManagerDbContext context)
            :base (context)
        {

        }

        public IQueryable<Tournament> GetTournamentQueryable(int tournamentId)
        {
            return DbContext.Tournaments
                .Where(x => x.TournamentId == tournamentId);
        }

        public IQueryable<Tournament> GetTournamentQueryable()
        {
            return DbContext.Tournaments;
        }

        public async Task<List<Tournament>> GetAllTournamentsAsync(int page, int pageSize)
        {
            return await DbContext.Tournaments
                .Where(x => x.Active && x.TournamentAccessId == (int)Core.Enums.TournamentAccess.Public)
                .Select(x => new Tournament
                {
                    TournamentId = x.TournamentId,
                    Avenue = x.Avenue,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    Name = x.Name,
                    Rules = x.Rules,
                    TeamsCount = x.TeamsCount,
                    TournamentParticipants = x.TournamentParticipants
                        .Where(tmt => tmt.TeamId != null).ToList()
                })
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Tournament>> GetUserTournaments(int userId)
        {
            return await DbContext.Tournaments.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<List<Tournament>> GetUserTournaments2(int userId)
        {
            return await DbContext.Tournaments.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<Tournament> GetTournamentById(int tournamentId)
        {
            return await DbContext.Tournaments.FirstOrDefaultAsync(x => x.TournamentId == tournamentId);
        }

        public async Task<List<Tournament>> GetTournamentWithUserParticipation(int userId)
        {
            return await DbContext.Tournaments
                .Where(t => t.TournamentParticipants
                    .Any(tp => tp.Team.Members
                        .Any(m => m.Id == userId)))
                .Select(x => new Tournament
                {
                    TournamentId = x.TournamentId,
                    Avenue = x.Avenue,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    Name = x.Name,
                    Rules = x.Rules,
                    TeamsCount = x.TeamsCount,
                    TournamentParticipants = x.TournamentParticipants
                        .Where(tmt => tmt.TeamId != null).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<TournamentMatch>> GetTournamentMatchesBySequenceId(int teamSequenceId, int tournamentId)
        {
            return await DbContext.TournamentMatches
               .Where(x => x.TournamentId == tournamentId &&
                    (x.HomeTeamSequenceId == teamSequenceId || x.AwayTeamSequenceId == teamSequenceId))
               .ToListAsync();
        }

        public async Task<int> GetLastRoundNumber(int tournamentId)
        {
            return await DbContext.TournamentMatches
                .Where(x => x.TournamentId == tournamentId)
                .MaxAsync(x => x.SequenceId);
        }
    }
}
