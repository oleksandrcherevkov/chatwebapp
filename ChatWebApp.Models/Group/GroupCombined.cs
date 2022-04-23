using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp.Models.Group
{
    public class GroupCombined
    {
        public int UserId { get; set; }
        public int Page { get; set; }
        public GroupWithMembers GroupInfo { get; set; }
        public ICollection<GroupMessageBlock> Messages { get; set; }
    }
}
