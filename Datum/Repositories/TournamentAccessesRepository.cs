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
    public class TournamentAccessesRepository: Repository<TournamentAccess>, ITournamentAccessesRepository
    {
        public TournamentAccessesRepository(FootballManagerDbContext context): base(context)
        {

        }

        public async Task<List<TournamentAccess>> GetTournamentAccesses()
        {
            return await DbContext.TournamentAccesses.ToListAsync();
        }
    }
}
