using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datum.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(FootballManagerDbContext context): base(context)
        {

        }

        public async Task<List<NotificationType>> GetNotificationTypes()
        {
            return await DbContext.NotificationTypes.ToListAsync();
        }

        public async Task<List<Notification>> GetUserNotifications(int userId)
        {
            return await DbContext.Notifications.Where(x => x.Pending).ToListAsync();
        }
    }
}
