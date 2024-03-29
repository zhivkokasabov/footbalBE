﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Core.Models
{
    public class User : IdentityUser<int>
    {
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public bool Active { get; set; }
        public List<UserPosition> UserPositions { get; set; }
        public Team Team { get; set; }
        public int? TeamId { get; set; }
        public List<Notification> Notifications { get; set; }
        public bool IsTeamCaptain { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}