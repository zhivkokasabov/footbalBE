using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datum.Repositories
{
    public class TournamentPlacementRepository: Repository<TournamentPlacement>, ITournamentPlacementRepository
    {
        public TournamentPlacementRepository(FootballManagerDbContext context) : base(context)
        {

        }
    }
}
