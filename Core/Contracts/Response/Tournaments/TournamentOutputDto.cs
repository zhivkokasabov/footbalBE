using Core.Contracts.Response.Tournaments;
using System;
using System.Collections.Generic;

namespace Core.contracts.Response
{
    public class TournamentOutputDto
    {
        public int? TournamentId { get; set; }
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
        public int EnrolledTeams { get; set; }
        public bool CanEdit { get; set; }
        public bool CanEditMatches { get; set; }
        public bool EliminationPhaseStarted { get; set; }
        public bool HasFinished { get; set; }
        public List<TournamentMatchOutputDto> TournamentMatches { get; set; } = new List<TournamentMatchOutputDto>();
        public List<TournamentParticipantOutputDto> TournamentParticipants { get; set; } = new List<TournamentParticipantOutputDto>();
    }
}
