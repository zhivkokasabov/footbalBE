using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Response.Teams
{
    public class TournamentPlacementOutputDto
    {
        public DateTime StartDate { get; set; }
        public string TournamentName { get; set; }
        public string Placement { get; set; }
    }
}
