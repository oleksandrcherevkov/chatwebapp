using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp.Services.Chat
{
    public class GetChatOptions
    {
        public int UserId { get; set; }
        public int ResponcerId { get; set; }
        public int PageNum { get; set; }
        public int PageSize { get; set; }
    }
}
