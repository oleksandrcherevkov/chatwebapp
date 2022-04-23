using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp.Models.Group
{
    public class GroupMessageBlock
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int GroupId { get; set; }
        public int? ResponseToMessageId { get; set; }
        public int? ResponseReceiverId { get; set; }
        public string? ResponseReceiverName { get; set; }
        public string SenderName { get; set; }
        public string Text { get; set; }
        public DateTime SendingTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public bool IsEdited { get; set; }
        public bool IsInvisible { get; set; }
        public bool IsHidden { get; set; }
    }
}
