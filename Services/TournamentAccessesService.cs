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
    public class TournamentAccessesService : ITournamentAccessesService
    {
        public IUnitOfWork UnitOfWork { get; }

        public TournamentAccessesService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<List<TournamentAccess>> GetTournamentAccesses()
        {
            return await UnitOfWork.TournamentAccesses.GetTournamentAccesses();
        }
    }
}
