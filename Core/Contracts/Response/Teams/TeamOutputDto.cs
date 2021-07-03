using Core.Contracts.Response.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Response.Teams
{
    public class TeamOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CaptainName { get; set; }
        public string EntryKey { get; set; }
        public List<UserOutputDto> Members { get; set; }
    }
}
