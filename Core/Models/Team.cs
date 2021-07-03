using System.Collections.Generic;

namespace Core.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string EntryKey { get; set; }
        public List<User> Members { get; set; }
        public List<TournamentMatchTeam> TournamentMatchTeams { get; set; }
        public List<TournamentParticipant> TournamentParticipants { get; set; }
    }
}
