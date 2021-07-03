using Core.contracts.response;
using Core.Contracts.Request.Tournaments;
using Core.Enums;
using Core.Models;
using System.Collections.Generic;

namespace Services.TournamentMatches.Factories
{
    public class MatchFactoryBuilder : IMatchFactoryBuilder
    {
        private readonly IMatchFactoryBuilder instance;
        public List<TournamentMatch> Matches {
            get
            {
                return instance.Matches;
            }
        }

        public List<ErrorModel> Errors
        {
            get
            {
                return instance.Errors;
            }
            set { }
        }

        public MatchFactoryBuilder(
            Tournament tournament,
            MatchResultDto result,
            int matchId
            )
        {
            switch (tournament.TournamentTypeId)
            {
                case (int)TournamentTypes.Elimination:
                    instance = new EliminationTournamentMatch(tournament, matchId, result);
                    break;
                default:
                    instance = new DefaultTournamentMatch(tournament, matchId, result);
                    break;
            }
        }
        public void UpdateParticipants()
        {
            instance.UpdateParticipants();
        }

        public void UpdateMatch()
        {
            instance.UpdateMatch();
        }
    }
}
