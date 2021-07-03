using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentAccessesController: ControllerBase
    {
        public ITournamentAccessesService TournamentAccessesService { get; }

        public TournamentAccessesController(ITournamentAccessesService tournamentAccessesService)
        {
            TournamentAccessesService = tournamentAccessesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TournamentAccess>>> GetTournamentAccesses()
        {
            var result = await TournamentAccessesService.GetTournamentAccesses();

            return Ok(result);
        }
    }
}
