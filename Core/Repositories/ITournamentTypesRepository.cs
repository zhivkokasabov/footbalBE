﻿using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ITournamentTypesRepository: IRepository<TournamentType>
    {
        public Task<List<TournamentType>> GetTournamentTypes();
    }
}
