using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Services.Tournaments.Factories
{
    public class ClassicTournament : ITournament
    {
        private List<char> groups = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
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
            var round = 1;

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
                            Round = round,
                            SequenceId = count,
                            StartTime = TournamentModel.StartDate
                        });

                        if (count % matchesPerRound == 0)
                        {
                            round++;
                        }

                        count++;
                    }
                }
            }
        }

        public void CreateParticipants()
        {
            TournamentModel.TournamentParticipants = new List<TournamentParticipant>();

            int numberOfGroups = TournamentModel.TeamsCount / TournamentModel.GroupSize;

            for (int ii = 0; ii < numberOfGroups; ii++)
            {
                for (int jj = 1; jj <= TournamentModel.GroupSize; jj++)
                {
                    TournamentModel.TournamentParticipants.Add(new TournamentParticipant
                    {
                        SequenceId = ii * TournamentModel.GroupSize + jj,
                        Group = groups[ii]
                    });
                }
            }
        }
    }
}
