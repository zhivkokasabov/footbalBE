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

        public async Task<List<Tournament>> GetAllTournamentsAsync(int page, int pageSize)
        {
            return await DbContext.Tournaments
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
                        .Any(m => m.Id == userId))).ToListAsync();
        }

        public async Task<List<TournamentMatch>> GetTournamentMatchesBySequenceId(int teamSequenceId, int tournamentId)
        {
            return await DbContext.TournamentMatches
               .Where(x => x.TournamentId == tournamentId &&
                    (x.HomeTeamSequenceId == teamSequenceId || x.AwayTeamSequenceId == teamSequenceId))
               .ToListAsync();
        }
    }
}
