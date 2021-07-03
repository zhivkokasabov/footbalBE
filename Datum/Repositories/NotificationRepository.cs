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

        public async Task<int> GetIncomingNotificationsCount(int userId)
        {
            return await DbContext.Notifications
                .Where(x => x.Active && x.ReceiverId == userId)
                .CountAsync();
        }

        public async Task<List<NotificationType>> GetNotificationTypes()
        {
            return await DbContext.NotificationTypes.ToListAsync();
        }

        public async Task<Notification> GetHasNotRequestedToJoinTournamentBefore(int userId, int tournamentId)
        {
            return await DbContext.Notifications
                .FirstOrDefaultAsync(x => x.SenderId == userId && x.EntityId == tournamentId);
        }

        public async Task<int> GetOutgoinNotificationsCount(int userId)
        {
            return await DbContext.Notifications
                .Where(x => x.Active && x.SenderId == userId)
                .CountAsync();
        }

        public async Task<List<Notification>> GetUserNotifications(int userId)
        {
            return await DbContext.Notifications
                .Where(x => x.Active && x.ReceiverId == userId).ToListAsync();
        }
    }
}
