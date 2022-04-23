using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp.Models.Chat
{
    public class ChatCombined
    {
        public int UserId { get; set; }
        public int Page { get; set; }
        public ChatViewBlock ResponcerInfo { get; set; }
        public ICollection<ChatMessageBlock> Messages { get; set; }
    }
}
