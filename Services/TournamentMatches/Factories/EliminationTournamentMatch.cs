using Core.contracts.response;
using Core.Contracts.Request.Tournaments;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.TournamentMatches.Factories
{
    public class EliminationTournamentMatch : IMatchFactoryBuilder
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
        public List<TournamentMatch> Matches { get; private set; }
        private TournamentMatch Match { get; set; }
        private List<TournamentParticipant> Participants { get; }
        private MatchResultDto Result { get; }
        private MatchResultDto OldResult { get; }

        public EliminationTournamentMatch(
            Tournament tournament,
            int matchId,
            MatchResultDto result)
        {
            if (result.HomeTeamScore == result.AwayTeamScore)
            {
                Errors.Add(new ErrorModel
                {
                    Error = "Elimination matches cannot end in a draw"
                });

                return;
            }

            Matches = tournament.TournamentMatches;
            Match = tournament.TournamentMatches.Find(x => x.TournamentMatchId == matchId);
            Participants = tournament.TournamentParticipants;
            Result = result;

            if (Match.Result != null)
            {
                var score = Match.Result.Split(':');
                var homeTeamScore = int.Parse(score[0]);
                var awayTeamScore = int.Parse(score[1]);

                OldResult = new MatchResultDto
                {
                    HomeTeamScore = homeTeamScore,
                    AwayTeamScore = awayTeamScore
                };
            }
        }

        public void UpdateMatch()
        {
            if (Errors.Any())
            {
                return;
            }

            Match.Result = $"{Result.HomeTeamScore} : {Result.AwayTeamScore}";
        }

        public void UpdateParticipants()
        {
            if (Errors.Any())
            {
                return;
            }

            var lastRound = Matches.Max(x => x.Round);

            if (lastRound == Match.Round)
            {
                return;
            }

            TournamentParticipant winner = Participants
                .First(x => Result.HomeTeamScore > Result.AwayTeamScore ?
                    x.SequenceId == Match.HomeTeamSequenceId :
                    x.SequenceId == Match.AwayTeamSequenceId);

            UpdateWinner(winner);
        }

        private void UpdateWinner(TournamentParticipant winner)
        {
            var round = Match.Round + 1;
            var orderedRoundMatches = Matches
                .Where(x => x.Round == Match.Round && x.IsEliminationMatch)
                .OrderBy(x => x.SequenceId)
                .ToList();
            var index = Math.Ceiling((orderedRoundMatches.IndexOf(Match) / 1.00 + 1) / 2) - 1;
            var match = Matches
                .Where(x => x.Round == Match.Round + 1)
                .OrderBy(x => x.SequenceId)
                .ToList()
                .ElementAt((int)index);
            var nextMatch = Matches.Find(x => x.TournamentMatchId == match.TournamentMatchId);

            if (OldResult != null)
            {
                TournamentParticipant oldWinner = Participants
                .First(x => OldResult.HomeTeamScore > OldResult.AwayTeamScore ?
                    x.SequenceId == Match.HomeTeamSequenceId :
                    x.SequenceId == Match.AwayTeamSequenceId);

                var matchTeam = nextMatch.TournamentMatchTeams.Find(x => x.TeamId == oldWinner.TeamId && x.Active == true);

                if (nextMatch.HomeTeamSequenceId == oldWinner.SequenceId)
                {
                    nextMatch.HomeTeamSequenceId = winner.SequenceId;
                }
                else
                {
                    nextMatch.AwayTeamSequenceId = winner.SequenceId;
                }

                matchTeam.TeamId = winner.TeamId ?? 0;
            }
            else
            {
                if (Match.SequenceId % 2 == 0)
                {
                    nextMatch.AwayTeamSequenceId = winner.SequenceId;
                    nextMatch.TournamentMatchTeams.Add(new TournamentMatchTeam
                    {
                        IsHomeTeam = false,
                        TournamentMatchId = nextMatch.TournamentMatchId,
                        TeamId = winner.TeamId ?? 0
                    });
                }
                else
                {
                    nextMatch.HomeTeamSequenceId = winner.SequenceId;
                    nextMatch.TournamentMatchTeams.Add(new TournamentMatchTeam
                    {
                        IsHomeTeam = true,
                        TournamentMatchId = nextMatch.TournamentMatchId,
                        TeamId = winner.TeamId ?? 0
                    });
                }
            }
        }
    }
}
