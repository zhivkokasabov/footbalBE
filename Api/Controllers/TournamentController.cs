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
using System;
using Core.Contracts.Request.Tournaments;
using Core.contracts.response;

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
        public async Task<ActionResult<List<TournamentOutputDto>>> GetAllTournaments([FromQuery] int pageSize, int page)
        {
            var tournamentsList = await TournamentService.GetAllTournaments(pageSize, page);

            return Ok(Mapper.Map<List<TournamentOutputDto>>(tournamentsList));
        }

        [HttpGet("{tournamentId}")]
        public async Task<ActionResult<List<TournamentOutputDto>>> GetTournamentById(int tournamentId)
        {
            var tournament = await TournamentService.GetTournamentById(tournamentId);

            if (tournament == null)
            {
                return BadRequest();
            }

            return Ok(Mapper.Map<TournamentOutputDto>(tournament));
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
            var matches = await TournamentService.GetTournamentMatches(tournamentId);

            return Ok(matches);
        }

        [HttpGet("user-tournaments")]
        public async Task<ActionResult<List<Tournament>>> GetUserTournaments()
        {
            var tournamentsList = await TournamentService.GetUserTournaments(userId, role);

            return Ok(tournamentsList);
        }

        [HttpPut("{tournamentId}/join-tournament")]
        public async Task<ActionResult<List<Tournament>>> GetUserTournaments([FromBody] JoinTournamentDto joinTournamentDto, int tournamentId)
        {
            var errorsList = await TournamentService.JoinTournament(tournamentId, joinTournamentDto.TeamId);

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
    }
}