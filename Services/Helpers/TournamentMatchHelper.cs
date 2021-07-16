using Core.Contracts.Response.Tournaments;
using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Services.Helpers
{
    public static class TournamentMatchHelper
    {
        public static IEnumerable<IGrouping<int, TournamentMatchOutputDto>> GroupMatches(List<TournamentMatchOutputDto> matches)
        {
            return matches.GroupBy(x => x.Round).OrderBy(x => x.ElementAt(0).Round);
        }
    }
}
