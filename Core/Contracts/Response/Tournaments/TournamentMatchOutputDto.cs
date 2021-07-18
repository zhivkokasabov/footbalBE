using Core.Models;
using System;
using System.Collections.Generic;

namespace Core.Contracts.Response.Tournaments
{
    public class TournamentMatchOutputDto
    {
        public int TournamentMatchId { get; set; }
        public int HomeTeamSequenceId { get; set; }
        public int AwayTeamSequenceId { get; set; }
        public DateTime StartTime { get; set; }
        public int Round { get; set; }
        public int SequenceId { get; set; }
        public int TournamentId { get; set; }
        public string Result { get; set; }
        public bool IsEliminationMatch { get; set; }
        public bool CanEdit { get; set; }
        public List<TournamentMatchTeamOutputDto> TournamentMatchTeams { get; set; } = new List<TournamentMatchTeamOutputDto>();
    }
}
