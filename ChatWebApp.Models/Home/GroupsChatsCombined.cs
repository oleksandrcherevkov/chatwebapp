
using ChatWebApp.Models.Chat;
using ChatWebApp.Models.Group;
using System.Collections.Generic;

namespace ChatWebApp.Models.Home
{
    public class GroupsChatsCombined 
    {
        public int UserId { get; set; }
        public ICollection<ChatViewBlock> Chats { get; set; }
        public ICollection<GroupViewBlock> Groups { get; set; }
    }
}
