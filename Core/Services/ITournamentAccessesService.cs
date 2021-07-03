using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITournamentAccessesService
    {
        public Task<List<TournamentAccess>> GetTournamentAccesses();
    }
}
