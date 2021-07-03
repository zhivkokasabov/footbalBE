using System.Collections.Generic;

namespace Core.Models
{
    public class TournamentParticipant
    {
        public int TournamentParticipantId { get; set; }
        public int SequenceId { get; set; }
        public char? Group { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
