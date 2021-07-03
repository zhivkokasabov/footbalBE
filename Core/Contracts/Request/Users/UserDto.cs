using Core.Models;
using System.Collections.Generic;

namespace Core.Contracts.Request
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<PlayerPosition> Positions { get; set; }
        public List<Role> Roles { get; set; }
    }
}
