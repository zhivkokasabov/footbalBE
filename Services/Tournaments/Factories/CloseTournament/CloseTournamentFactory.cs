using Core.Enums;
using Core.Models;
using System.Collections.Generic;

namespace Services.Tournaments.Factories.CloseTournament
{
    class CloseTournamentFactory: ICloseTournament
    {
        private readonly ICloseTournament instance;
        public CloseTournamentFactory(Tournament tournament)
        {
            switch (tournament.TournamentTypeId)
            {
                case (int)TournamentTypes.Elimination:
                    instance = new EliminationTournament(tournament);
                    break;
                case (int)TournamentTypes.Classic:
                    instance = new ClassicTournament(tournament);
                    break;
                default:
                    instance = new DefaultTournament(tournament);
                    break;
            }
        }

        public List<TournamentPlacement> CloseTournament()
        {
            return instance.CloseTournament();
        }
    }
}
