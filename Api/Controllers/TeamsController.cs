using AutoMapper;
using Core.contracts.response;
using Core.Contracts.Request.Teams;
using Core.Contracts.Response.Teams;
using Core.Contracts.Response.Tournaments;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController: ControllerBase
    {
        private ITeamsService TeamsService { get; }
        public IMapper Mapper { get; }

        private int userId;
            
        public TeamsController(
            ITeamsService teamsService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            TeamsService = teamsService;
            Mapper = mapper;
            userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpPost("")]
        [Authorize]
        public async Task<ActionResult<TeamOutputDto>> CreateTeam([FromBody] TeamDto team)
        {
            var result = await TeamsService.CreateTeam(team, userId);

            return Ok(Mapper.Map<Team, TeamOutputDto>(result));
        }

        [HttpGet("{teamId}")]
        [Authorize]
        public async Task<ActionResult<TeamOutputDto>> GetTeam(int teamId)
        {
            var result = await TeamsService.GetTeam(teamId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Team, TeamOutputDto>(result));
        }

        [HttpGet("user-team")]
        [Authorize]
        public async Task<ActionResult<TeamOutputDto>> GetUserTeam()
        {
            var result = await TeamsService.GetUserTeam(userId);

            return Ok(Mapper.Map<Team, TeamOutputDto>(result));
        }

        [HttpGet("{teamId}/matches")]
        [Authorize]
        public async Task<ActionResult<List<TournamentMatchOutputDto>>> GetTeamMatches(int teamId)
        {
            var result = await TeamsService.GetTeamMatches(teamId);
            var mapped = Mapper.Map<List<TournamentMatchOutputDto>>(result);

            return Ok(mapped.GroupBy(x => x.StartTime));
        }

        [HttpPost("{entryKey}/join")]
        [Authorize]
        public async Task<ActionResult<TeamOutputDto>> AddUserToTeam(string entryKey)
        {
            var result = await TeamsService.AddUserToTeam(userId, entryKey);

            if (result == null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Error = "Invalid Entry Key",
                            FieldName = "Entry Key"
                        }
                    }
                });
            }

            return Ok(Mapper.Map<Team, TeamOutputDto>(result));
        }
    }
}
