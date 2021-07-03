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

        public async Task<List<TournamentMatch>> GetTournamentMatches(int tournamentId)
        {
            return await DbContext.TournamentMatches
                .Where(x => x.TournamentId == tournamentId)
                .ToListAsync();
        }
    }
}
