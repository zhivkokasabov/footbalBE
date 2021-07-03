using System;

namespace Core.contracts.Request
{
    public class TournamentDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Avenue { get; set; }
        public string Description { get; set; }
        public int TeamsCount { get; set; }
        public int? TeamsAdvancingAfterGroups { get; set; }
        public DateTime StartDate { get; set; }
        public string Rules { get; set; }
        public int PlayingFields { get; set; }
        public int MatchLength { get; set; }
        public int HalfTimeLength { get; set; }
        public int? GroupSize { get; set; }
        public int PlayingDaysId { get; set; }
        public int TournamentAccessId { get; set; }
        public int TournamentTypeId { get; set; }
    }
}
