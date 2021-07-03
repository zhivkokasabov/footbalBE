using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TournamentRepository: Repository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(FootballManagerDbContext context)
            :base (context)
        {

        }

        public async Task<IEnumerable<Tournament>> GetAllTournamentsAsync()
        {
            return await DbContext.Tournaments
                .ToListAsync();
        }

        private FootballManagerDbContext DbContext
        {
            get { return Context as FootballManagerDbContext; }
        }
    }
}
