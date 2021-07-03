namespace Core.Models
{
    public class UserPosition: Base
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PlayerPositionId { get; set; }
        public PlayerPosition PlayerPosition { get; set; }
    }
}
