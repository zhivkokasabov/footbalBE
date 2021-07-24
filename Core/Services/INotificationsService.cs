using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface INotificationsService
    {
        Task<List<NotificationType>> GetNotificationTypes();
        Task<List<Notification>> GetUserNotifications(int userId);
    }
}
