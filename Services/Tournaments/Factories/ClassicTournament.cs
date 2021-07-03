using Core.Enums;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Tournaments.Factories
{
    public class ClassicTournament : Base, ITournament
    {
        // Will need better solution for group characters. There is a validation for the maximum
        // possible groups count.
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
            OrderTournamentMatches();
            SetMatchStart(
                TournamentModel,
                TournamentModel.TournamentMatches,
                new DateTime(
                    TournamentModel.StartDate.Year,
                    TournamentModel.StartDate.Month,
                    TournamentModel.StartDate.Day,
                    TournamentModel.FirstMatchStartAt.Hour,
                    TournamentModel.FirstMatchStartAt.Minute,
                    0
                ),
                false);

            var lastNonEliminationMatch = TournamentModel.TournamentMatches.OrderBy(x => x.StartTime).Last();

            CreateEliminationMatches(count);
            SetMatchStart(lastNonEliminationMatch);
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

        private void OrderTournamentMatches()
        {
            // Classic tournament matches are created sequentially per team. E.g the first couple of matches are all
            // the matches that the participant with SequenceId 1 is going to play during the group stage
            // SetStartTime prevents conflicting matches and is going to add one match per time slot because of that
            // despite the number of playing fields.
            // That is why we order them each time to have the greatest possible distance between their matches.
            var totalMatches = TournamentModel.TournamentMatches.Count;
            var orderedMatches = new List<TournamentMatch>();
            var cycleHashset = new HashSet<int>();
            var participants = TournamentModel.TournamentParticipants
                .Select(x => x.SequenceId)
                .ToList();

            while (orderedMatches.Count != totalMatches)
            {
                participants.ForEach(participantId =>
                {
                    var match = TournamentModel.TournamentMatches
                        .FirstOrDefault(x => (x.HomeTeamSequenceId == participantId || x.AwayTeamSequenceId == participantId) &&
                            !(cycleHashset.Contains(x.HomeTeamSequenceId)) && !(cycleHashset.Contains(x.AwayTeamSequenceId)));

                    if (match != null)
                    {
                        orderedMatches.Add(match);
                        TournamentModel.TournamentMatches.Remove(match);
                        cycleHashset.Add(match.HomeTeamSequenceId);
                        cycleHashset.Add(match.AwayTeamSequenceId);
                    }
                });

                cycleHashset = new HashSet<int>();
            }

            TournamentModel.TournamentMatches = orderedMatches;
        }

        private void SetMatchStart(TournamentMatch lastNonEliminationMatch)
        {
            var count = 0;
            // Last non elimination match start at:
            var lastMatchDateTime = lastNonEliminationMatch.StartTime;

            var eliminationMatches = TournamentModel.TournamentMatches
                .Where(x => x.IsEliminationMatch)
                .ToList();

            // Provide unique identifier to elimination matches. Otherwise 0 until ProceedToEliminations is triggered.
            // Required by SetMatchStart
            eliminationMatches.ForEach(x =>
                 {
                     x.HomeTeamSequenceId = ++count;
                     x.AwayTeamSequenceId = ++count;
                 });

            // Set startTime separetely per group to avoid conflicts
            var groupedEliminationMatches = eliminationMatches.GroupBy(x => x.Round);

            foreach (var group in groupedEliminationMatches)
            {
                SetMatchStart(TournamentModel, group.ToList(), lastMatchDateTime, true);

                lastMatchDateTime = group.Last().StartTime;
            }

            // revert changes to sequenceId's actual will be set on ProceedToEliminations
            eliminationMatches.ForEach(x =>
                {
                    x.HomeTeamSequenceId = 0;
                    x.AwayTeamSequenceId = 0;
                });
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
