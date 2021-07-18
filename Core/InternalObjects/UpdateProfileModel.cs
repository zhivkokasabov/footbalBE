using Core.contracts.response;
using Core.Models;
using System.Collections.Generic;

namespace Core.InternalObjects
{
    public class UpdateProfileModel
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
        public User User { get; set; }
    }
}
