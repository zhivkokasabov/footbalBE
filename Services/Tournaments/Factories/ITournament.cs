using Core.Models;

namespace Services.Tournaments.Factories
{
    public interface ITournament
    {
        public Tournament TournamentModel { get; set; }
        public void CreateParticipants();
        public void CreateTournamentMatches();
    }
}
