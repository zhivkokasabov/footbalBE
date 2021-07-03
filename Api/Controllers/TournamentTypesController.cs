using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentTypesController: ControllerBase
    {
        public ITournamentTypesService TournamentTypesService { get; }

        public TournamentTypesController(ITournamentTypesService tournamentTypesService)
        {
            TournamentTypesService = tournamentTypesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TournamentType>>> GetTournamentTypes()
        {
            var result = await TournamentTypesService.GetTournamentTypes();

            return Ok(result);
        }
    }
}
