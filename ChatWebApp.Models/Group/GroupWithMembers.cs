using ChatWebApp.Models.Chat;
using ChatWebApp.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp.Models.Group
{
    public class GroupWithMembers : GroupViewBlock
    {
        public ICollection<ChatViewBlock> Members { get; set; }
    }
}
