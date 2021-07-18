﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Core.Models
{
    public class TournamentMatch
    {
        public int TournamentMatchId { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public int HomeTeamSequenceId { get; set; }
        public int AwayTeamSequenceId { get; set; }
        public DateTime StartTime { get; set; }
        public int Round { get; set; }
        public int SequenceId { get; set; }
        public string Result { get; set; }
        public bool IsEliminationMatch { get; set; }
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public List<TournamentMatchTeam> TournamentMatchTeams { get; set; } = new List<TournamentMatchTeam>();
    }
}
