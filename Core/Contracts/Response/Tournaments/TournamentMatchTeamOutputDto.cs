using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Response.Tournaments
{
    public class TournamentMatchTeamOutputDto
    {
        public int TournamentMatchTeamId { get; set; }
        public int TeamId { get; set; }
        public int TournamentMatchId { get; set; }
        public bool IsHomeTeam { get; set; }
        public string Name { get; set; }
    }
}
