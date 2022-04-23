using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp.Services.Group
{
    public class GetGroupOptions
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int PageNum { get; set; }
        public int PageSize { get; set; }
    }
}
