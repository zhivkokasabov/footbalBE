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

        public async Task<List<TournamentParticipant>> GetTournamentParticipants(int tournamentId)
        {
            return await DbContext.TournamentParticipants
                    .Where(x => x.TournamentId == tournamentId)
                    .ToListAsync();
        }
    }
}
