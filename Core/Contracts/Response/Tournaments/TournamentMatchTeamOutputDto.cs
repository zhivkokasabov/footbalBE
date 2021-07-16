using Core.Contracts.Response.Teams;

namespace Core.Contracts.Response.Tournaments
{
    public class TournamentMatchTeamOutputDto
    {
        public int TournamentMatchTeamId { get; set; }
        public int TeamId { get; set; }
        public TeamOutputDto Team { get; set; }
        public int TournamentMatchId { get; set; }
        public bool IsHomeTeam { get; set; }
        public string Name { get; set; }
    }
}
