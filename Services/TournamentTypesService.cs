using Core.Models;
using Core.Repositories;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class TournamentTypesService : ITournamentTypesService
    {
        public IUnitOfWork UnitOfWork { get; }

        public TournamentTypesService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }


        public async Task<List<TournamentType>> GetTournamentTypes()
        {
            return await UnitOfWork.TournamentTypes.GetTournamentTypes();
        }
    }
}
