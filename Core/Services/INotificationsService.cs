using Core.contracts.response;
using Core.Contracts.Response.Notifications;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface INotificationsService
    {
        Task<List<NotificationType>> GetNotificationTypes();
        Task<List<NotificationDto>> GetUserNotifications(int userId);
        Task<NotificationsCountDto> GetNotificationsCount(int userId);
        Task<List<ErrorModel>> ResolveRequest(NotificationDto notification, int userId);
    }
}
