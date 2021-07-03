using System.Collections.Generic;

namespace Core.Models
{
    public class PlayerPosition
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public List<UserPosition> UserPositions {get; set;}
    }
}
