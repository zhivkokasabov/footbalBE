using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Base
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; }

        public Base()
        {
            Active = true;
            CreatedAt = new DateTime();
            UpdatedAt = new DateTime();
        }
    }
}
