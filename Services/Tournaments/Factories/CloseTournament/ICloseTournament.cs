using Core.Models;
using System.Collections.Generic;

namespace Services.Tournaments.Factories.CloseTournament
{
    interface ICloseTournament
    {
        public List<TournamentPlacement> CloseTournament();
    }
}
