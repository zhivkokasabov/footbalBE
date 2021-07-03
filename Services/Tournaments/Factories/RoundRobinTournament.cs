using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Tournaments.Factories
{
    public class RoundRobinTournament: Base, ITournament
    {
        private Tournament Tournament;
        public RoundRobinTournament(Tournament tournament)
        {
            Tournament = tournament;
        }

        public Tournament TournamentModel { get => Tournament; set => Tournament = value; }

        public void CreateParticipants()
        {
            TournamentModel.TournamentParticipants = new List<TournamentParticipant>();

            for (int ii = 0; ii < TournamentModel.TeamsCount; ii++)
            {
                TournamentModel.TournamentParticipants.Add(new TournamentParticipant
                {
                    SequenceId = ii,
                });
            }
        }

        public void CreateTournamentMatches()
        {
            TournamentModel.TournamentMatches = new List<TournamentMatch>();
            
            if (TournamentModel.TeamsCount % 2 == 1)
            {
                TournamentModel.TeamsCount += 1;
            }

            var matchesPerRound = TournamentModel.TeamsCount / 2;
            var topRow = Enumerable.Range(1, matchesPerRound).ToArray();
            var bottomRow = Enumerable.Range(matchesPerRound + 1, matchesPerRound).Reverse().ToArray();
            var round = 1;
            var sequenceId = 1;

            while (round < TournamentModel.TeamsCount)
            {
                for (int ii = 0; ii < matchesPerRound; ii++)
                {
                    TournamentModel.TournamentMatches.Add(new TournamentMatch
                    {
                        HomeTeamSequenceId = topRow[ii],
                        AwayTeamSequenceId = bottomRow[ii],
                        Round = round,
                        SequenceId = sequenceId,
                        StartTime = DateTime.Now
                    });

                    sequenceId += 1;
                }

                var lastTeam = topRow[matchesPerRound - 1];
                var firstTeam = bottomRow[0];

                for (var ii = matchesPerRound - 1; ii >= 1; ii--)
                {
                    topRow[ii] = topRow[ii - 1];
                }

                for (var ii = 0; ii < matchesPerRound - 1; ii++)
                {
                    bottomRow[ii] = bottomRow[ii + 1];
                }

                topRow[1] = firstTeam;
                bottomRow[matchesPerRound - 1] = lastTeam;

                round += 1;
            }

            SetMatchStart(TournamentModel, TournamentModel.TournamentMatches, TournamentModel.StartDate, false);
        }
    }
}
