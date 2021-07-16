using AutoMapper;
using Core.contracts.response;
using Core.contracts.Response;
using Core.Contracts.Response.Tournaments;
using Core.Enums;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using Services.Tournaments.Factories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class TournamentService : ITournamentsService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        public TournamentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<Tournament> CreateTournament(Tournament tournament, int userId)
        {
            var tournamentBuilder = new TournamentFactoryBuilder(tournament);

            tournament.UserId = userId;
            tournamentBuilder.CreateParticipants();
            tournamentBuilder.CreateTournamentMatches();

            await UnitOfWork.Tournaments.AddAsync(tournament);
            await UnitOfWork.CommitAsync();

            return tournament;
        }

        public async Task<List<Tournament>> GetUserTournaments(int userId, string role)
        {
            if (role == Roles.Organization)
            {
                return await UnitOfWork.Tournaments.GetUserTournaments(userId);
            }

            return await UnitOfWork.Tournaments.GetTournamentWithUserParticipation(userId);
        }

        public async Task<List<Tournament>> GetAllTournaments(int pageSize, int page)
        {
            return await UnitOfWork.Tournaments.GetAllTournamentsAsync(pageSize, page);
        }

        public async Task<TournamentOutputDto> GetTournamentById(int tournamentId, int userId)
        {
            var tournament = await UnitOfWork.Tournaments.GetTournamentById(tournamentId);

            if (tournament == null)
            {
                return null;
            }

            var tournamentOutput = Mapper.Map<TournamentOutputDto>(tournament);

            tournamentOutput.CanEdit = tournament.UserId == userId;
            tournamentOutput.CanEditMatches = tournament.UserId == userId;

            return tournamentOutput;
        }

        public async Task<List<TournamentParticipant>> GetTournamentParticipants(int tournamentId)
        {
            var participants = await UnitOfWork.TournamentParticipants.GetTournamentParticipants(tournamentId);

            return OrderParticipants(participants);
        }

        public async Task<IEnumerable<IGrouping<int, TournamentMatchOutputDto>>> GetTournamentMatches(int tournamentId)
        {
            var matches = await UnitOfWork.TournamentMatches.GetTournamentMatches(tournamentId);
            var matchesOutput = Mapper.Map<List<TournamentMatchOutputDto>>(matches);

            return TournamentMatchHelper.GroupMatches(matchesOutput);
        }

        public async Task<List<ErrorModel>> JoinTournament(int tournamentId, int userId)
        {
            var errors = new List<ErrorModel>();
            var team = await UnitOfWork.Teams.GetUserTeam(userId);
            var tournament = await UnitOfWork.Tournaments.GetTournamentById(tournamentId);

            if (team == null)
            {
                errors.Add(new ErrorModel { Error = "Team does not exists" });
                return errors;
            }

            if (tournament.TournamentAccessId == (int)Core.Enums.TournamentAccess.Public)
            {
                var participant = await UnitOfWork.TournamentParticipants.FindUnassignedParticipant(tournamentId);

                if (participant == null)
                {
                    errors.Add(new ErrorModel { Error = "No available slots in the tournament" });
                    return errors;
                }

                var matches = await UnitOfWork.Tournaments.GetTournamentMatchesBySequenceId(participant.SequenceId, tournamentId);
                participant.TeamId = team.TeamId;

                foreach (var match in matches)
                {
                    var isHomeTeam = match.HomeTeamSequenceId == participant.SequenceId;

                    match.TournamentMatchTeams.Add(
                        new TournamentMatchTeam { 
                            IsHomeTeam = isHomeTeam,
                            TeamId = team.TeamId,
                        });
                }

                await UnitOfWork.CommitAsync();
            }
            else
            {
                errors.Add(new ErrorModel { Error = "Cannot join non public tournament" });
                return errors;
            }

            return errors;
        }

        public async Task<bool> GetUserIsAllowedToParticipate(int tournamentId, int userId)
        {
            var user = await UnitOfWork.Users.GetUserAsync(userId);
            var tournamentParticipants = await UnitOfWork.TournamentParticipants.GetTournamentParticipants(tournamentId);
            var tournamentParticipant = tournamentParticipants.Find(x => x.TeamId == user.TeamId);
            var availableSlots = tournamentParticipants.Find(x => x.TeamId == null);

            return user.IsTeamCaptain && tournamentParticipant == null && availableSlots != null;
        }

        public async Task<bool> GetCanProceedToEliminations(int tournamentId, int userId)
        {
            var tournament = await UnitOfWork.Tournaments.GetByIdAsync(tournamentId);

            if (tournament.UserId != userId)
            {
                return false;
            }

            var matches = UnitOfWork.TournamentMatches
                .Find(x => x.TournamentId == tournamentId && string.IsNullOrEmpty(x.Result) && !x.IsEliminationMatch);

            if (matches.Any())
            {
                return false;
            }

            return true;
        }

        public async Task<ErrorResponse> ProceedToEliminations(int tournamentId, int userId)
        {
            var response = new ErrorResponse();
            var tournament = await UnitOfWork.Tournaments.GetByIdAsync(tournamentId);

            if (tournament.TournamentTypeId != (int)TournamentTypes.Classic)
            {
                response.Errors.Add(new ErrorModel
                {
                    Error = "Only classic tournament can proceed to eliminations"
                });
            }

            if (tournament.UserId != userId)
            {
                response.Errors.Add(new ErrorModel
                {
                    Error = "Only tournament organizer can proceed to eliminations"
                });
            }

            var participants = await UnitOfWork.TournamentParticipants.GetTournamentParticipants(tournamentId);
            var eliminationsParticipants = ProceedToEliminations(participants, tournament);
            var eliminationMatches = await UnitOfWork.TournamentMatches.GetEliminationTournamentMatches(tournamentId);

            var tournamentMatches = UpdateMatchesParticipants(eliminationsParticipants, eliminationMatches);

            tournament.EliminationPhaseStarted = true;
            
            UnitOfWork.Tournaments.Update(tournament);
            UnitOfWork.TournamentMatches.Update(tournamentMatches);
            await UnitOfWork.CommitAsync();

            return response;
        }

        public async Task<ErrorResponse> CloseTournament(int tournamentId, int userId)
        {
            var response = new ErrorResponse();
            var tournament = await UnitOfWork.Tournaments.GetByIdAsync(tournamentId);
            var matches = await UnitOfWork.TournamentMatches.GetTournamentMatches(tournamentId);
            var canClose = matches.Any(x => x.Result == null) == false;

            if (tournament.UserId != userId)
            {
                response.Errors.Add(new ErrorModel
                {
                    Error = "Only tournament organizer can proceed to eliminations"
                });

                return response;
            }

            if (canClose == false)
            {
                response.Errors.Add(new ErrorModel
                {
                    Error = "Tournament can not be finished, because not all matches are finished"
                });

                return response;
            }

            if (tournament.HasFinished)
            {
                response.Errors.Add(new ErrorModel
                {
                    Error = "Tournament has already finished"
                });
                
                return response;
            }

            tournament.HasFinished = true;

            return null;
        }

        private List<TournamentMatch> UpdateMatchesParticipants(
            List<TournamentParticipant> participants,
            List<TournamentMatch> matches)
        {
            var orderedParticipants = OrderParticipants(participants);
            var orderedMatches = matches.OrderBy(x => x.Round);
            var numberOfMatches = orderedParticipants.Count / 2;

            for (int ii = 0; ii < numberOfMatches; ii++)
            {
                var homeTeam = orderedParticipants.ElementAt(ii);
                var awayTeam = orderedParticipants.ElementAt(numberOfMatches * 2 - 1 - ii);
                var match = orderedMatches.ElementAt(ii);

                match.HomeTeamSequenceId = homeTeam.SequenceId;
                match.AwayTeamSequenceId = awayTeam.SequenceId;
                match.TournamentMatchTeams = new List<TournamentMatchTeam>
                {
                    new TournamentMatchTeam { IsHomeTeam = true, TournamentMatchId = match.TournamentMatchId, TeamId = (int)homeTeam.TeamId },
                    new TournamentMatchTeam { TournamentMatchId = match.TournamentMatchId, TeamId = (int)awayTeam.TeamId },
                };
            }

            return matches;
        }

        private List<TournamentParticipant> ProceedToEliminations(
            List<TournamentParticipant> tournamentParticipants,
            Tournament tournament)
        {
            var groupedTeams = tournamentParticipants.GroupBy(x => x.Group);
            var orderedTeams = groupedTeams
                .Select(x => OrderParticipants(x.ToList()));
            var nextPowOfTwo = NextPowOfTwo(groupedTeams.Count() * tournament.TeamsAdvancingAfterGroups);
            var advancingTeams = new List<TournamentParticipant>();
            var bestNextCount = nextPowOfTwo - groupedTeams.Count() * tournament.TeamsAdvancingAfterGroups;

            foreach (var group in orderedTeams)
            {
                advancingTeams.AddRange(
                    group.GetRange(0, tournament.TeamsAdvancingAfterGroups).ToList());
            }

            if (bestNextCount > 0)
            {
                var candidatesForElimination = orderedTeams
                    .Select(x => x.ToList()
                    .ElementAt(tournament.TeamsAdvancingAfterGroups)
                    ).ToList();
                var bestNextTeams = OrderParticipants(candidatesForElimination).GetRange(0, bestNextCount);

                advancingTeams.AddRange(bestNextTeams);
            }

            return advancingTeams;
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

        private List<TournamentParticipant> OrderParticipants(List<TournamentParticipant> participants)
        {
            return participants.OrderByDescending(x => x.Points)
                    .ThenByDescending(x => x.GoalDifference)
                    .ThenBy(x => x.Goals)
                    .ThenBy(x => x.Wins)
                    .ToList();
        }
    }
}
