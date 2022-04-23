using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp.Models.Chat
{
    public class ChatMessageCombined
    {
        public int UserId { get; set; }
        public ChatMessageBlock Message { get; set; }
    }
}
