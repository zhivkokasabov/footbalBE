using Core.Contracts.Response.Tournaments;
using Core.Enums;
using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Services.Helpers
{
    public static class TournamentParticipantsHelper
    {
        public static List<TournamentParticipant> OrderParticipants(List<TournamentParticipant> participants)
        {
            return participants.OrderByDescending(x => x.Points)
                    .ThenByDescending(x => x.GoalDifference)
                    .ThenBy(x => x.Goals)
                    .ThenBy(x => x.Wins)
                    .ToList();
        }
    }
}
