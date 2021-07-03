using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Response.Users
{
    public class UserOutputDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public bool IsTeamCaptain { get; set; }
        public List<PlayerPosition> Positions { get; set; }
        public List<Role> Roles { get; set; }
    }
}
