using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Response
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public Meta Meta { get; set; }
    }

    public class Meta
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
