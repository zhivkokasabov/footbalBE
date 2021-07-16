using Core.contracts.response;
using Core.Models;
using System.Collections.Generic;

namespace Services.TournamentMatches.Factories
{
    public interface IMatchFactoryBuilder
    {
        public List<ErrorModel> Errors { get; set; }
        public List<TournamentMatch> Matches { get; }
        public void UpdateParticipants();
        public void UpdateMatch();
    }
}
