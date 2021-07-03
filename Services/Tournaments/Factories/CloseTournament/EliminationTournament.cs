using Core;
using Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Services.Tournaments.Factories.CloseTournament
{
    public class EliminationTournament : ICloseTournament
    {
        private int TournamentId { get; }
        private List<TournamentMatch> Matches { get; set; }

        public EliminationTournament(Tournament tournament)
        {
            TournamentId = tournament.TournamentId;
            Matches = tournament.TournamentMatches;
        }

        public List<TournamentPlacement> CloseTournament()
        {
            var lastRound = Matches.Max(x => x.Round);
            var groupedAndOrderedMatches = Matches.OrderByDescending(x => x.Round).GroupBy(x => x.Round);
            var placedTeams = new List<TournamentPlacement>();
            var placeTeamsIds = new HashSet<int>();
            var count = 1;

            foreach (var round in groupedAndOrderedMatches)
            {
                if (round.Key == lastRound)
                {
                    var placementFromFinal = PlaceFinal(round.ElementAt(0), TournamentId);

                    placementFromFinal.Select(x => x.TeamId).ToList().ForEach(x => placeTeamsIds.Add(x));

                    placedTeams.AddRange(placementFromFinal);
                }
                else if (round.Key == lastRound - 1)
                {
                    var placementFromSemiFinal = PlaceSemiFinals(round.ToList(), TournamentId, placeTeamsIds);

                    placementFromSemiFinal.Select(x => x.TeamId).ToList().ForEach(x => placeTeamsIds.Add(x));

                    placedTeams.AddRange(placementFromSemiFinal);
                }
                else
                {
                    var roundPlacement = PlaceRound(round.ToList(), TournamentId, count, placeTeamsIds);

                    placedTeams.AddRange(roundPlacement);
                }

                count += round.Count();
            }

            return placedTeams;
        }

        private List<TournamentPlacement> PlaceRound(List<TournamentMatch> roundMatches, int tournamentId, int count, HashSet<int> alreadyPlaced)
        {
            var result = new List<TournamentPlacement>();
            var placement = $"{count + 1}th-{count + roundMatches.Count()}th";

            roundMatches.ForEach(match =>
            {
                match.TournamentMatchTeams.ForEach(team =>
                {
                    if (alreadyPlaced.Any(x => x == team.TeamId) == false)
                    {
                        result.Add(new TournamentPlacement
                        {
                            Placement = placement,
                            TournamentId = tournamentId,
                            TeamId = team.TeamId
                        });
                    }
                });
            });

            return result;
        }

        private List<TournamentPlacement> PlaceSemiFinals(List<TournamentMatch> roundMatches, int tournamentId, HashSet<int> alreadyPlaced)
        {
            var result = new List<TournamentPlacement>();

            roundMatches.ForEach(match =>
            {
                match.TournamentMatchTeams.ForEach(team =>
                {
                    if (alreadyPlaced.Any(x => x == team.TeamId) == false)
                    {
                        result.Add(new TournamentPlacement
                        {
                            Placement = "3rd-4th",
                            TournamentId = tournamentId,
                            TeamId = team.TeamId
                        });
                    }
                });
            });

            return result;
        }

        private List<TournamentPlacement> PlaceFinal(TournamentMatch match, int tournamentId)
        {
            var matchScore = match.Result
                .Split(Constants.ResultSeparator)
                .Select(x => int.Parse(x));
            var result = new List<TournamentPlacement>();
            var homeTeam = match.TournamentMatchTeams.Find(x => x.IsHomeTeam);
            var awayTeam = match.TournamentMatchTeams.Find(x => !x.IsHomeTeam);

            if (matchScore.ElementAt(0) > matchScore.ElementAt(1))
            {
                result.Add(new TournamentPlacement
                {
                    Placement = "1st",
                    TeamId = homeTeam.TeamId,
                    TournamentId = tournamentId,
                });

                result.Add(new TournamentPlacement
                {
                    Placement = "2nd",
                    TeamId = awayTeam.TeamId,
                    TournamentId = tournamentId,
                });
            }
            else
            {
                result.Add(new TournamentPlacement
                {
                    Placement = "2nd",
                    TeamId = homeTeam.TeamId,
                    TournamentId = tournamentId,
                });

                result.Add(new TournamentPlacement
                {
                    Placement = "1st",
                    TeamId = awayTeam.TeamId,
                    TournamentId = tournamentId,
                });
            }

            return result;
        }
    }
}
