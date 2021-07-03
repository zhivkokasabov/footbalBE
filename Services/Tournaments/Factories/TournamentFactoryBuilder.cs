using Core.Enums;
using Core.Models;

namespace Services.Tournaments.Factories
{
    public class TournamentFactoryBuilder: ITournament
    {
        private readonly ITournament instance;
        public TournamentFactoryBuilder(Tournament tournament)
        {
            switch (tournament.TournamentTypeId)
            {
                case (int)Core.Enums.TournamentTypes.Elimination:
                    instance = new EliminationTournament(tournament);
                    break;
                case (int)Core.Enums.TournamentTypes.Classic:
                    instance = new ClassicTournament(tournament);
                    break;
                case (int)Core.Enums.TournamentTypes.RoundRobin:
                    instance = new RoundRobinTournament(tournament);
                    break;
                default:
                    break;
            }
        }

        public Tournament TournamentModel { get => instance.TournamentModel; set => instance.TournamentModel = value; }

        public void CreateParticipants()
        {
            instance.CreateParticipants();
        }

        public void CreateTournamentMatches()
        {
            instance.CreateTournamentMatches();
        }
    }
}
