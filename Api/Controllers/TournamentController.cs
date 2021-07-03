using Api.Controllers;
using AutoMapper;
using Core.contracts.Response;
using Core.contracts.Request;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Contracts.Response.Tournaments;
using System.Linq;
using Core.contracts.response;
using Core.Contracts.Request.Tournaments;
using Core.Contracts.Response;

namespace Api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentController : ControllerBase
    {
        private readonly int userId;
        private readonly string role;
        private readonly ITournamentsService TournamentService;
        private readonly IMapper Mapper;

        public TournamentController(
            ITournamentsService tournamentService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            TournamentService = tournamentService;
            Mapper = mapper;
            userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            role = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        [HttpPost("")]
        public async Task<ActionResult<TournamentOutputDto>> CreateTournament([FromBody] TournamentDto tournamentDto)
        {
            var tournament = Mapper.Map<Tournament>(tournamentDto);
            var newTournament = await TournamentService.CreateTournament(tournament, userId);

            return Ok(Mapper.Map<TournamentOutputDto>(newTournament));
        }

        [HttpGet("")]
        public async Task<ActionResult<PagedResult<TournamentOutputDto>>> GetAllTournaments([FromQuery] int page, int pageSize)
        {
            var pagedResult = await TournamentService.GetAllTournaments(pageSize, page);

            return Ok(pagedResult);
        }

        [HttpGet("{tournamentId}")]
        public async Task<ActionResult<List<TournamentOutputDto>>> GetTournamentById(int tournamentId)
        {
            var tournament = await TournamentService.GetTournamentById(tournamentId, userId);

            if (tournament == null)
            {
                return BadRequest();
            }

            return Ok(tournament);
        }

        [HttpGet("{tournamentId}/participants")]
        public async Task<ActionResult<List<TournamentParticipantOutputDto>>> GetTournamentParticipants(int tournamentId)
        {
            var participants = await TournamentService.GetTournamentParticipants(tournamentId);

            return Ok(Mapper.Map<List<TournamentParticipantOutputDto>>(participants));
        }

        [HttpGet("{tournamentId}/matches")]
        public async Task<ActionResult<IEnumerable<IGrouping<int, TournamentMatchOutputDto>>>> GetTournamentMatches(int tournamentId)
        {
            var matches = await TournamentService.GetTournamentMatches(tournamentId, userId);

            return Ok(matches);
        }

        [HttpGet("user-tournaments")]
        public async Task<ActionResult<List<TournamentOutputDto>>> GetUserTournaments()
        {
            var tournamentsList = await TournamentService.GetUserTournaments(userId, role);

            return Ok(Mapper.Map<List<TournamentOutputDto>>(tournamentsList));
        }

        [HttpPut("{tournamentId}/join-tournament")]
        public async Task<ActionResult<ErrorResponse>> JoinTournament(int tournamentId)
        {
            var errorsList = await TournamentService.JoinTournament(tournamentId, userId);

            if (errorsList.Any())
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = errorsList
                });
            }

            return Ok();
        }

        [HttpPut("{tournamentId}/leave")]
        public async Task<ActionResult<ErrorResponse>> LeaveTournament(int tournamentId)
        {
            var errorsList = await TournamentService.LeaveTournament(tournamentId, userId);

            if (errorsList.Any())
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = errorsList
                });
            }

            return Ok();
        }

        [HttpPost("{tournamentId}/request-to-join-tournament")]
        public async Task<ActionResult<List<Tournament>>> RequestToJoinTournament([FromBody] Notification notification, int tournamentId)
        {
            var errorsList = await TournamentService.RequestToJoinTournament(notification, tournamentId, userId);

            if (errorsList.Any())
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = errorsList
                });
            }

            return Ok();
        }

        [HttpGet("{tournamentId}/user-is-allowed")]
        public async Task<ActionResult<List<Tournament>>> GetUserIsAllowedToParticipate(int tournamentId)
        {
            var userIsAllowed = await TournamentService.GetUserIsAllowedToParticipate(tournamentId, userId);

            return Ok(userIsAllowed);
        }

        [HttpGet("{tournamentId}/can-proceed-to-eliminations")]
        public async Task<ActionResult<bool>> GetCanProceedToEliminations(int tournamentId)
        {
            var canProceed = await TournamentService.GetCanProceedToEliminations(tournamentId, userId);

            return Ok(canProceed);
        }

        [HttpPost("{tournamentId}/proceed-to-eliminations")]
        public async Task<ActionResult<IEnumerable<IGrouping<int, TournamentMatchOutputDto>>>> ProceedToEliminations(int tournamentId)
        {
            var response = await TournamentService.ProceedToEliminations(tournamentId, userId);

            if (response.Errors.Any())
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = response.Errors
                });
            }

            return Ok(response.Matches);
        }

        [HttpPut("{tournamentId}/close")]
        public async Task<ActionResult<TournamentOutputDto>> CloseTournament(int tournamentId)
        {
            var response = await TournamentService.CloseTournament(tournamentId, userId);

            if (response.Errors.Any())
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = response.Errors
                });
            }

            return Ok(response.Tournament);
        }

        [HttpPut("{tournamentId}/start")]
        public async Task<ActionResult<ErrorResponse>> StartTournament(int tournamentId)
        {
            var errors = await TournamentService.StartTournament(tournamentId, userId);

            if (errors.Any())
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = errors
                });
            }

            return Ok();
        }

        [HttpPut("{tournamentId}/request-to-join/{teamId}")]
        public async Task<ActionResult<TournamentOutputDto>> RequestToJoinTournament(int tournamentId, int teamId)
        {
            var response = await TournamentService.CloseTournament(tournamentId, userId);

            if (response.Errors.Any())
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = response.Errors
                });
            }

            return Ok(response.Tournament);
        }
    }
}