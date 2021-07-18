using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Datum.Repositories
{
    public class PlayerPositionRepository: Repository<PlayerPosition>, IPlayerPositionsRepository
    {
        public PlayerPositionRepository(FootballManagerDbContext context): base(context)
        {}

        public async Task<List<PlayerPosition>> GetPlayerPositions()
        {
            return await DbContext.PlayerPositions.ToListAsync();
        }
    }
}
