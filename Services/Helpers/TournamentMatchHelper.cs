using Core.Contracts.Response.Tournaments;
using Core.Enums;
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

        public static List<TournamentMatchOutputDto> UpdateMatchesCanEdit(List<TournamentMatchOutputDto> matches, Tournament tournament)
        {
            if (tournament.HasStarted == false)
            {
                matches.ForEach(x => x.CanEdit = false);

                return matches;
            }

            matches.ForEach(x =>
            {
                x.CanEdit = false;

                if (x.HomeTeamSequenceId != 0 && x.AwayTeamSequenceId != 0)
                {
                    x.CanEdit = true;
                }
                else
                {
                    return;
                }

                if (tournament.TournamentTypeId == (int)TournamentTypes.Classic)
                {
                    if (x.IsEliminationMatch && tournament.EliminationPhaseStarted)
                    {
                        x.CanEdit = true;
                    }

                    if (x.IsEliminationMatch == false && tournament.EliminationPhaseStarted == false)
                    {
                        x.CanEdit = true;
                    }

                    if (x.IsEliminationMatch == false && tournament.EliminationPhaseStarted)
                    {
                        x.CanEdit = false;
                    }
                }
            });

            return matches;
        }
    }
}
