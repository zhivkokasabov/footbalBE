using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPlayerPositionsService
    {
        Task<List<PlayerPosition>> GetPlayerPositions();
    }
}
