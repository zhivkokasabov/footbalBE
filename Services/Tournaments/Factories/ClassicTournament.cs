using Core.Enums;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Tournaments.Factories
{
    public class ClassicTournament : ITournament
    {
        private List<char> groups = new() { '-', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        private Tournament Tournament;
        public ClassicTournament(Tournament tournament)
        {
            Tournament = tournament;
        }

        public Tournament TournamentModel { get => Tournament; set => Tournament = value; }

        public void CreateTournamentMatches()
        {
            TournamentModel.TournamentMatches = new List<TournamentMatch>();

            var groups = TournamentModel.TournamentParticipants
                .GroupBy(x => x.Group);
            var matchesPerRound = TournamentModel.PlayingFields;
            var count = 1;

            foreach (var group in groups)
            {
                for (int ii = 0; ii < group.Count(); ii++)
                {
                    for (int jj = ii + 1; jj < group.Count(); jj++)
                    {
                        TournamentModel.TournamentMatches.Add(new TournamentMatch
                        {
                            HomeTeamSequenceId = group.ElementAt(ii).SequenceId,
                            AwayTeamSequenceId = group.ElementAt(jj).SequenceId,
                            Round = 1,
                            SequenceId = count,
                            StartTime = TournamentModel.StartDate
                        });

                        count++;
                    }
                }
            }

            // in case a group is not full remove matches created for non existent teams
            TournamentModel.TournamentMatches = TournamentModel.TournamentMatches.Where(x => x.HomeTeamSequenceId != 0).ToList();

            CreateEliminationMatches(count);
        }

        public void CreateParticipants()
        {
            TournamentModel.TournamentParticipants = new List<TournamentParticipant>();

            var numberOfGroups = Math.Ceiling(TournamentModel.TeamsCount / 1.00 / TournamentModel.GroupSize);
            var count = 1;
            var group = 1;

            while (count <= TournamentModel.TeamsCount)
            {
                TournamentModel.TournamentParticipants.Add(new TournamentParticipant
                {
                    SequenceId = count,
                    Group = groups[group]
                });

                if (group == numberOfGroups)
                {
                    group = 1;
                }
                else
                {
                    group++;
                }

                count++;
            }
        }

        private void CreateEliminationMatches(int sequenceId)
        {
            var numberOfGroups = Math.Ceiling(TournamentModel.TeamsCount / 1.00 / TournamentModel.GroupSize);
            var nextPowOfTwo = NextPowOfTwo((int)numberOfGroups * TournamentModel.TeamsAdvancingAfterGroups);
            var tournamentBuilder = new TournamentFactoryBuilder(new Tournament
            {
                TournamentTypeId = (int)TournamentTypes.Elimination,
                TeamsCount = nextPowOfTwo
            });

            tournamentBuilder.CreateTournamentMatches();
            tournamentBuilder.TournamentModel.TournamentMatches.ForEach(x =>
            {
                x.HomeTeamSequenceId = 0;
                x.AwayTeamSequenceId = 0;
                x.SequenceId = x.SequenceId + sequenceId;
                x.IsEliminationMatch = true;
                x.Round = x.Round + 1;
            });

            TournamentModel.TournamentMatches.AddRange(tournamentBuilder.TournamentModel.TournamentMatches);
        }

        private int NextPowOfTwo(int x)
        {
            var result = 2;

            while (result < x)
            {
                result *= 2;
            }

            return result;
        }
    }
}
