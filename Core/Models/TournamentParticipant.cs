using System.Collections.Generic;

namespace Core.Models
{
    public class TournamentParticipant
    {
        public int TournamentParticipantId { get; set; }
        public int SequenceId { get; set; }
        public char? Group { get; set; }
        public int Played { get; set; } = 0;
        public int Wins { get; set; } = 0;
        public int Loses { get; set; } = 0;
        public int Draws { get; set; } = 0;
        public int Points { get; set; } = 0;
        public int Goals { get; set; } = 0;
        public int ConceivedGoals { get; set; } = 0;
        public int GoalDifference { get; set; } = 0;

        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
