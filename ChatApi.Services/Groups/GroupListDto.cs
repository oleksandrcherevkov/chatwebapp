using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.Services.Users;

namespace ChatApi.Services.Groups
{
    public class GroupListDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int MembersCount { get; set; }
        public string GroupImageUrl { get; set; }
    }
}
