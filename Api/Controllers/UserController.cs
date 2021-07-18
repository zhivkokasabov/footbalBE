using AutoMapper;
using Core.contracts.response;
using Core.Contracts.Request;
using Core.Contracts.Response;
using Core.Contracts.Response.Users;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        public IUsersService UserService { get; }
        public IRolesService RoleService { get; }
        public IMapper Mapper { get; }

        public UserController(IUsersService userService, IRolesService roleService, IMapper mapper)
        {
            UserService = userService;
            RoleService = roleService;
            Mapper = mapper;
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<UserOutputDto>> GetUser()
        {
            return Ok(await UserService.GetUser(HttpContext.User.Identity.Name));
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<ActionResult<UserOutputDto>> UpdateUser(UserDto user)
        {
            var response = await UserService.UpdateUser(user);

            if (response.Errors.Any())
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = response.Errors
                });
            }

            var userDto = Mapper.Map<User, UserOutputDto>(response.User);

            return Ok(userDto);
        }

        [HttpPost("")]
        public async Task<ActionResult<UserOutputDto>> CreateUser([FromBody] UserDto user)
        {
            var result = await UserService.CreateUser(user);

            if (result.Errors != null)
            {
                return BadRequest(result.Errors);
            }

            var loggedInUser = Mapper.Map<User, UserOutputDto>(result.User);

            return Ok(new SuccessLogin
            {
                Token = UserService.GenerateToken(result.User),
                User = loggedInUser
            });
        } 

        [HttpPost("login")]
        public async Task<ActionResult<UserOutputDto>> Login([FromBody] UserDto user)
        {
            var result = await UserService.Login(user);

            if (result.Errors != null)
            {
                return BadRequest(result.Errors);
            }

            var loggedInUser = Mapper.Map<User, UserOutputDto>(result.User);

            return Ok(new SuccessLogin
            {
                Token = UserService.GenerateToken(result.User),
                User = loggedInUser
            });
        }

        [HttpGet("roles")]
        public async Task<ActionResult<List<RoleDto>>> GetRoles()
        {
            var roles = await RoleService.GetRoles();

            return Ok(roles);
        }

        class SuccessLogin
        {
            public string Token { get; set; }
            public UserOutputDto User { get; set; }
        }
    }
}
