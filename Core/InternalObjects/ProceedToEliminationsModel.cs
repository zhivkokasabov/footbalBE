using Core.contracts.response;
using Core.Contracts.Response.Tournaments;
using System.Collections.Generic;
using System.Linq;

namespace Core.InternalObjects
{
    public class ProceedToEliminationsModel
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
        public IEnumerable<IGrouping<int, TournamentMatchOutputDto>> Matches { get; set; }
    }
}
