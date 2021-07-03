using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerPositionController : ControllerBase
    {
        public IPlayerPositionsService PlayerPositionService { get; }

        public PlayerPositionController(IPlayerPositionsService playerPositionService)
        {
            PlayerPositionService = playerPositionService;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<PlayerPosition>>> GetPlayerPositions()
        {
            var playerPositions = await PlayerPositionService.GetPlayerPositions();

            return Ok(playerPositions);
        }
    }
}