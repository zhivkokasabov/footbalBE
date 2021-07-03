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
                case (int)Core.Enums.TournamentType.Elimination:
                    instance = new EliminationTournament(tournament);
                    break;
                case (int)Core.Enums.TournamentType.Classic:
                    instance = new ClassicTournament(tournament);
                    break;
                case (int)Core.Enums.TournamentType.RoundRobin:
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
