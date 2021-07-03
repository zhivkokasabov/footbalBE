namespace Core.Contracts.Response.Notifications
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Accepted { get; set; }
        public bool Rejected { get; set; }
        public string RedirectUrl { get; set; }
        public int SenderId { get; set; }
        public int EntityId { get; set; }
        public int NotificationTypeId { get; set; }
    }
}
