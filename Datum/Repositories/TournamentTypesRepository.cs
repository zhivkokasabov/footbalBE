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
    public class TournamentTypesRepository : Repository<TournamentType>, ITournamentTypesRepository
    {
        public TournamentTypesRepository(FootballManagerDbContext context): base(context)
        {
        }

        public async Task<List<TournamentType>> GetTournamentTypes()
        {
            return await DbContext.TournamentTypes.ToListAsync();
        }
    }
}
