using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IPlayerPositionsRepository: IRepository<PlayerPosition>
    {
        public Task<List<PlayerPosition>> GetPlayerPositions();
    }
}
