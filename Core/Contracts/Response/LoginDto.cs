using Core.contracts.response;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Response
{
    public class LoginDto
    {
        public User User { get; set; }
        public ErrorResponse Errors { get; set; }
    }
}
