using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<List<NotificationType>> GetNotificationTypes();
        Task<List<Notification>> GetUserNotifications(int userId);
        Task<int> GetIncomingNotificationsCount(int userId);
        Task<int> GetOutgoinNotificationsCount(int userId);
        Task<Notification> GetHasNotRequestedToJoinTournamentBefore(int userId, int tournamentId);
    }
}
