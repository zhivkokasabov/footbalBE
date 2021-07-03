using Core.Contracts.Response.Teams;
using Core.Models;
namespace Core.Contracts.Response.Tournaments
{
    public class TournamentParticipantOutputDto
    {
        public int TournamentParticipantId { get; set; }
        public int SequenceId { get; set; }
        public char? Group { get; set; }
        public int? TeamId { get; set; }
        public int? Played { get; set; }
        public int? Wins { get; set; }
        public int? Loses { get; set; }
        public int? Draws { get; set; }
        public int? Points { get; set; }
        public int Goals { get; set; } = 0;
        public int ConceivedGoals { get; set; } = 0;
        public int GoalDifference { get; set; } = 0;
        public TeamOutputDto Team { get; set; }
    }
}
