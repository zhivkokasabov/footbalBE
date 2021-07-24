using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int ReceiverId { get; set; }
        public bool Accepted { get; set; }
        public bool Pending { get; set; }
        public bool Rejected { get; set; }
        public string RedirectUrl { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
