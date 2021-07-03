using AutoMapper;
using Core.contracts.response;
using Core.Contracts.Request.Tournaments;
using Core.Contracts.Response.Tournaments;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentMatchesController: ControllerBase
    {
        public ITournamentMatchesService TournamentMatchesService { get; }
        public IMapper Mapper { get; }

        public TournamentMatchesController(ITournamentMatchesService tournamentMatchesService, IMapper mapper)
        {
            TournamentMatchesService = tournamentMatchesService;
            Mapper = mapper;
        }

        [HttpPost("{matchId}")]
        public async Task<ActionResult<List<TournamentMatchOutputDto>>> SaveTournamentMatch([FromBody] MatchResultDto matchResult, int matchId)
        {
            var result = await TournamentMatchesService.UpsertTournamentMatch(matchResult, matchId);

            if (result.Errors != null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = result.Errors
                });
            }

            return Ok(result.Matches);
        }

        [HttpPut("{matchId}")]
        public async Task<ActionResult<List<TournamentMatchOutputDto>>> UpdateTournamentMatch([FromBody] MatchResultDto matchResult, int matchId)
        {
            var result = await TournamentMatchesService.UpsertTournamentMatch(matchResult, matchId);

            if (result.Errors != null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = result.Errors
                });
            }

            return Ok(result.Matches);
        }
    }
}
