namespace Core.Models
{
    public class TournamentPlacement
    {
        public int TournamentPlacementId { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public string Placement { get; set; }
    }
}
