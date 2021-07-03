using Core.Contracts.Response;
using Core.Models;
using Datum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController
    {
        private readonly FootballManagerDbContext Context;
        private readonly UserManager<User> UserManager;

        public TokenController(FootballManagerDbContext context, UserManager<User> userManager)
        {
            Context = context;
            UserManager = userManager;
        }

        [HttpPost("")]
        public Task<IActionResult> Create([FromBody] LoginDto credentials)
        {
            //return new ObjectResult(await GenerateToken(credentials.Email));
            return null;
        }
    }
}
