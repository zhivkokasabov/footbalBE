using Core.Models;
using System;
using System.Collections.Generic;

namespace Services.Tournaments.Factories
{
    public class EliminationTournament : ITournament
    {
        private Tournament Tournament;
        public EliminationTournament(Tournament tournament)
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

            var round = 1;
            var numberOfRoundMatches = TournamentModel.TeamsCount / 2;
            var sequenceId = 1;

            while(numberOfRoundMatches >= 1)
            {
                for (int ii = 1; ii <= numberOfRoundMatches; ii += 1)
                {
                    TournamentModel.TournamentMatches.Add(new TournamentMatch
                    {
                        HomeTeamSequenceId = ii * 2 - 1,
                        AwayTeamSequenceId = ii * 2,
                        Round = round,
                        SequenceId = sequenceId,
                        StartTime = new DateTime()
                    });

                    sequenceId += 1;
                }

                numberOfRoundMatches = numberOfRoundMatches / 2;
                round += 1;
            }
        }
    }
}
