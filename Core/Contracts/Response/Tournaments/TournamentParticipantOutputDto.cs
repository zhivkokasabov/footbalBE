using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Response.Tournaments
{
    public class TournamentParticipantOutputDto
    {
        public int TournamentParticipantId { get; set; }
        public int SequenceId { get; set; }
        public char? Group { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
