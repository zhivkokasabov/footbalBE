using Core.contracts.response;
using Core.Contracts.Response.Tournaments;
using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Core.InternalObjects
{
    public class UpsertTournamentMatchModel
    {
        public List<ErrorModel> Errors { get; set; }
        public IEnumerable<IGrouping<int, TournamentMatchOutputDto>> Matches { get; set; }
    }
}
