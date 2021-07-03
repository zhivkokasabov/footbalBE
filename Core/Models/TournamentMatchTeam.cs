using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TournamentMatchTeam: Base
    {
        public int TournamentMatchTeamId { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int TournamentMatchId { get; set; }
        public TournamentMatch TournamentMatch { get; set; }
        public bool IsHomeTeam { get; set; }
    }
}
