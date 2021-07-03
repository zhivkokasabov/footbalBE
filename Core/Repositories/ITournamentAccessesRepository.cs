using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITournamentAccessesRepository: IRepository<TournamentAccess>
    {
        public Task<List<TournamentAccess>> GetTournamentAccesses();
    }
}
