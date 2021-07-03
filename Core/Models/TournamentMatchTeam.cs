using System.Diagnostics;

namespace Core.Models
{
    public class TournamentMatchTeam: Base
    {
        public int TournamentMatchTeamId { get; set; }
        [DebuggerDisplay("TeamId = {TeamId}")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int TournamentMatchId { get; set; }
        public TournamentMatch TournamentMatch { get; set; }
        public bool IsHomeTeam { get; set; }
    }
}
