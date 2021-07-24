using Core.Models;
using Core.Repositories;
using Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Notifications
{
    public class NotificationsService : INotificationsService
    {
        public IUnitOfWork UnitOfWork { get; }

        public NotificationsService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<List<NotificationType>> GetNotificationTypes()
        {
            return await UnitOfWork.Notifications.GetNotificationTypes();
        }

        public async Task<List<Notification>> GetUserNotifications(int userId)
        {
            return await UnitOfWork.Notifications.GetUserNotifications(userId);
        }
    }
}
