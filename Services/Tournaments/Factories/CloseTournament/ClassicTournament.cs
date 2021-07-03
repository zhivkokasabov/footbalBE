using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Tournaments.Factories.CloseTournament
{
    public class ClassicTournament : ICloseTournament
    {
        private int TournamentId { get; set; }
        private List<TournamentMatch> Matches { get; set; }
        private List<TournamentParticipant> Participants { get; set; }
        private Tournament Tournament { get; set; }

        public ClassicTournament(Tournament tournament)
        {
            TournamentId = tournament.TournamentId;
            Matches = tournament.TournamentMatches;
            Participants = tournament.TournamentParticipants;
            Tournament = tournament;
        }
        public List<TournamentPlacement> CloseTournament()
        {
            var placements = new List<TournamentPlacement>();
            var tournament = CreateSubEliminationTournament();
            var eliminationTournament = new EliminationTournament(tournament);

            placements.AddRange(eliminationTournament.CloseTournament());

            var placedTeamsIds = GetUniqueTeamIdsOfPlacedTeams(placements);

            var notPlaced = Participants.Where(x => placedTeamsIds.Contains(x.TeamId ?? 0) == false).ToList();
            var numberOfNotPlaced = notPlaced.Count();
            var numberOfPlaced = placements.Count();
            var placement = $"{numberOfPlaced + 1}th - {numberOfPlaced + numberOfNotPlaced}th";

            notPlaced.ForEach(x =>
            {
                placements.Add(new TournamentPlacement
                {
                    TeamId = x.TeamId ?? 0,
                    Placement = placement,
                    TournamentId = TournamentId
                });
            });

            return placements;
        }

        private Tournament CreateSubEliminationTournament()
        {
            return new Tournament
            {
                TournamentId = TournamentId,
                TournamentMatches = Matches.Where(x => x.IsEliminationMatch).ToList()
            };
        }

        private HashSet<int> GetUniqueTeamIdsOfPlacedTeams(List<TournamentPlacement> placements)
        {
            var placedTeamsIds = new HashSet<int>();

            placements.ForEach(x => placedTeamsIds.Add(x.TeamId));

            return placedTeamsIds;
        }
    }
}
