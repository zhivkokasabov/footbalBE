using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Tournament: Base
    {
        public int TournamentId { get; set; }
        public string Name { get; set; }
        public string Avenue { get; set; }
        public string Description { get; set; }
        public int TeamsCount { get; set; }
        public int TeamsAdvancingAfterGroups { get; set; }
        public DateTime StartDate { get; set; }
        public string Rules { get; set; }
        public int PlayingFields { get; set; }
        public TimeSpan MatchLength { get; set; }
        public TimeSpan HalfTimeLength { get; set; }
        public int GroupSize { get; set; }
        public bool EliminationPhaseStarted { get; set; }
        public bool HasFinished { get; set; }
        public PlayingDays PlayingDays { get; set; }
        public int PlayingDaysId { get; set; }
        public TournamentAccess TournamentAccess { get; set; }
        public int TournamentAccessId { get; set; }
        public TournamentType TournamentType { get; set; }
        public int TournamentTypeId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<TournamentParticipant> TournamentParticipants { get; set; }
        public List<TournamentMatch> TournamentMatches { get; set; }
    }
}
