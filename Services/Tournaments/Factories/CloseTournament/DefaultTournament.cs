using Core.Models;
using Services.Helpers;
using System.Collections.Generic;

namespace Services.Tournaments.Factories.CloseTournament
{
    public class DefaultTournament : ICloseTournament
    {
        private int TournamentId { get; }
        private List<TournamentParticipant> Participants { get; set; }

        public DefaultTournament(Tournament tournament)
        {
            TournamentId = tournament.TournamentId;
            Participants = tournament.TournamentParticipants;
        }

        public List<TournamentPlacement> CloseTournament()
        {
            var participants = TournamentParticipantsHelper.OrderParticipants(Participants);
            var count = participants.Count;
            var placements = new List<TournamentPlacement>();

            for (int ii = 0; ii < count; ii++)
            {
                switch (ii)
                {
                    case 0:
                        placements.Add(
                            CreateTournamentPlacement(TournamentId, participants[ii].TeamId, "1st"));
                        break;
                    case 1:
                        placements.Add(
                            CreateTournamentPlacement(TournamentId, participants[ii].TeamId, "2nd"));
                        break;
                    case 2:
                        placements.Add(
                            CreateTournamentPlacement(TournamentId, participants[ii].TeamId, "3rd"));
                        break;
                    default:
                        placements.Add(
                            CreateTournamentPlacement(TournamentId, participants[ii].TeamId, $"{ii}th"));
                        break;
                }
            }

            return placements;
        }

        private TournamentPlacement CreateTournamentPlacement(int tournamentId, int? teamId, string placement)
        {
            var tId = teamId ?? 0;

            return new TournamentPlacement
            {
                TournamentId = tournamentId,
                TeamId = tId,
                Placement = placement
            };
        }
    }
}
