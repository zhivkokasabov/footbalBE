using Core.Models;
using Core.Repositories;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PlayerPositionService: IPlayerPositionsService
    {
        public IUnitOfWork UnitOfWork { get; }

        public PlayerPositionService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<List<PlayerPosition>> GetPlayerPositions()
        {
            return await UnitOfWork.PlayerPositions.GetPlayerPositions();
        }
    }
}
