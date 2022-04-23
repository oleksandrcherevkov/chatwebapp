using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp.Models.Group
{
    public class GroupMessageCombined
    {
        public int UserId { get; set; }
        public GroupMessageBlock Message { get; set; }
    }
}
