using Core.contracts.response;
using Core.Contracts.Request.Tournaments;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.TournamentMatches.Factories
{
    public class DefaultTournamentMatch : IMatchFactoryBuilder
    {
        public List<TournamentMatch> Matches { get; private set; }
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
        private TournamentMatch Match { get; set; }
        private List<TournamentParticipant> Participants { get; }
        private MatchResultDto Result { get; }
        private MatchResultDto OldResult { get; }
        private Score Score { get; set; }
        private Score OldScore { get; set; }
        private Tournament Tournament { get; set; }

        public DefaultTournamentMatch(
            Tournament tournament,
            int matchId,
            MatchResultDto result
            )
        {
            Match = tournament.TournamentMatches.Find(x => x.TournamentMatchId == matchId);
            Matches = tournament.TournamentMatches;
            Participants = tournament.TournamentParticipants;
            Result = result;
            Tournament = tournament;
            Score = new Score
            {
                Goals = result.HomeTeamScore > result.AwayTeamScore ? result.HomeTeamScore : result.AwayTeamScore,
                Conceived = result.HomeTeamScore > result.AwayTeamScore ? result.AwayTeamScore : result.HomeTeamScore
            };

            if (Match.Result != null)
            {
                var score = Match.Result.Split(':');
                var homeTeamScore = int.Parse(score[0]);
                var awayTeamScore = int.Parse(score[1]);

                OldScore = new Score
                {
                    Goals = homeTeamScore > awayTeamScore ? homeTeamScore : awayTeamScore,
                    Conceived = homeTeamScore < awayTeamScore ? homeTeamScore : awayTeamScore
                };

                OldResult = new MatchResultDto
                {
                    HomeTeamScore = homeTeamScore,
                    AwayTeamScore = awayTeamScore
                };
            }
        }

        public void UpdateParticipants()
        {
            if (Match.IsEliminationMatch)
            {
                var eliminationMatch = new EliminationTournamentMatch(Tournament, Match.TournamentMatchId, Result);

                eliminationMatch.UpdateParticipants();

                Matches = eliminationMatch.Matches;

                return;
            }

            TournamentParticipant winner;
            TournamentParticipant loser;

            if (OldResult != null)
            {
                if (OldResult.HomeTeamScore > OldResult.AwayTeamScore)
                {
                    RevertWinner(Participants.First(x => x.SequenceId == Match.HomeTeamSequenceId));
                    RevertLoser(Participants.First(x => x.SequenceId == Match.AwayTeamSequenceId));
                }
                else if (OldResult.AwayTeamScore > OldResult.HomeTeamScore)
                {
                    RevertWinner(Participants.First(x => x.SequenceId == Match.AwayTeamSequenceId));
                    RevertLoser(Participants.First(x => x.SequenceId == Match.HomeTeamSequenceId));
                }
                else
                {
                    RevertDraw(
                        Participants.First(x => x.SequenceId == Match.AwayTeamSequenceId),
                        Participants.First(x => x.SequenceId == Match.HomeTeamSequenceId));
                }
            }

            if (Result.HomeTeamScore > Result.AwayTeamScore)
            {
                winner = Participants.First(x => x.SequenceId == Match.HomeTeamSequenceId);
                loser = Participants.First(x => x.SequenceId == Match.AwayTeamSequenceId);

                UpdateWinner(winner);
                UpdateLoser(loser);
            }
            else if (Result.AwayTeamScore > Result.HomeTeamScore)
            {
                loser = Participants.First(x => x.SequenceId == Match.HomeTeamSequenceId);
                winner = Participants.First(x => x.SequenceId == Match.AwayTeamSequenceId);

                UpdateWinner(winner);
                UpdateLoser(loser);
            }
            else
            {
                UpdateDraw(Participants.First(x => x.SequenceId == Match.AwayTeamSequenceId),
                        Participants.First(x => x.SequenceId == Match.HomeTeamSequenceId));
            }
        }

        public void UpdateMatch()
        {
            Match.Result = $"{Result.HomeTeamScore} : {Result.AwayTeamScore}";
        }

        private void RevertLoser(TournamentParticipant loser)
        {
            loser.Loses -= 1;
            loser.Goals -= OldScore.Conceived;
            loser.ConceivedGoals -= OldScore.Goals;
            RevertCommon(loser);
        }

        private void RevertWinner(TournamentParticipant winner)
        {
            winner.Wins -= 1;
            winner.Goals -= OldScore.Goals;
            winner.ConceivedGoals -= OldScore.Conceived;
            winner.Points -= 3;
            RevertCommon(winner);
        }

        private void RevertDraw(TournamentParticipant team1, TournamentParticipant team2)
        {
            team1.Points -= 1;
            team1.Goals -= OldScore.Goals;
            team1.ConceivedGoals -= OldScore.Conceived;
            team1.Draws -= 1;

            team2.Points -= 1;
            team2.Goals -= OldScore.Goals;
            team2.ConceivedGoals -= OldScore.Conceived;
            team2.Draws -= 1;

            RevertCommon(team1);
            RevertCommon(team2);
        }

        private void UpdateLoser(TournamentParticipant loser)
        {
            loser.Loses += 1;
            loser.Goals += Score.Conceived;
            loser.ConceivedGoals += Score.Goals;
            SetCommon(loser);
        }

        private void UpdateWinner(TournamentParticipant winner)
        {
            winner.Wins += 1;
            winner.Points += 3;
            winner.Goals += Score.Goals;
            winner.ConceivedGoals += Score.Conceived;

            SetCommon(winner);
        }

        private void UpdateDraw(TournamentParticipant team1, TournamentParticipant team2)
        {
            team1.Points += 1;
            team1.Goals += Score.Goals;
            team1.ConceivedGoals += Score.Conceived;
            team1.Draws += 1;

            team2.Points += 1;
            team2.Goals += Score.Goals;
            team2.ConceivedGoals += Score.Conceived;
            team2.Draws += 1;

            SetCommon(team1);
            SetCommon(team2);
        }

        private void SetCommon(TournamentParticipant participant)
        {
            participant.GoalDifference = participant.Goals - participant.ConceivedGoals;
            participant.Played += 1;
        }

        private void RevertCommon(TournamentParticipant participant)
        {
            participant.GoalDifference = participant.Goals - participant.ConceivedGoals;
            participant.Played -= 1;
        }
    }

    class Score
    {
        public int Goals { get; set; }
        public int Conceived { get; set; }
    }
}
