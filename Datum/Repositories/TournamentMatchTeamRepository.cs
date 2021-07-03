using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datum.Repositories
{
    public class TournamentMatchTeamRepository : Repository<TournamentMatchTeam>, ITournamentMatchTeamRepository
    {
        public TournamentMatchTeamRepository(FootballManagerDbContext context): base(context)
        {

        }

        public async Task<List<TournamentMatchTeam>> GetTournamentMatchTeamsByTeamId(int teamId)
        {
            return await DbContext.TournamentMatchTeams
                .Where(x => x.Active && x.TeamId == teamId)
                .ToListAsync();
        }
    }
}
