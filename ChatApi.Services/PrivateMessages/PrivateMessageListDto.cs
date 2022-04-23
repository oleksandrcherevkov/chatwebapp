using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.PrivateMessages
{
    public class PrivateMessageListDto
    {
        public int Id { get; set; }
        public int SenderId  { get; set; }
        public int ReceiverId { get; set; }
        public int? ResponseToId { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
        public bool IsInvisiblе { get; set; }
        public DateTime SendingTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public bool IsEdited { get; set; } 
    }
}
