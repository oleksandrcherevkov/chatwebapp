using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.Services.Users;

namespace ChatApi.Services.Groups
{
    public class GroupGetDto : GroupListDto
    {
        public ICollection<UserListDto> Members { get; set; }
    }
}
