using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ITournamentTypesService
    {
        public Task<List<TournamentType>> GetTournamentTypes();
    }
}
