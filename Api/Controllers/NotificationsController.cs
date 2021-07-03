using Core.contracts.response;
using Core.Contracts.Response.Notifications;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController: ControllerBase
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly INotificationsService NotificationsService;
        private readonly int userId;

        public NotificationsController(
            IHttpContextAccessor httpContextAccessor,
            INotificationsService notificationsService
            )
        {
            HttpContextAccessor = httpContextAccessor;
            NotificationsService = notificationsService;
            userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet("notification-types")]
        [Authorize]
        public async Task<ActionResult<List<NotificationType>>> GetNotificationTypes()
        {
            var result = await NotificationsService.GetNotificationTypes();

            return Ok(result);
        }

        [HttpGet("")]
        [Authorize]
        public async Task<ActionResult<List<NotificationDto>>> GetUserNotifications()
        {
            var result = await NotificationsService.GetUserNotifications(userId);

            return Ok(result);
        }

        [HttpGet("count")]
        [Authorize]
        public async Task<ActionResult<NotificationsCountDto>> GetNotificationsCount()
        {
            var result = await NotificationsService.GetNotificationsCount(userId);

            return Ok(result);
        }

        [HttpPut("")]
        [Authorize]
        public async Task<ActionResult<List<ErrorModel>>> ResolveRequest([FromBody] NotificationDto notification)
        {
            var result = await NotificationsService.ResolveRequest(notification, userId);
            
            if (result.Any())
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = result
                });
            }

            return Ok();
        }
    }
}
