using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.EF.Models
{
    public class PrivateMessage : MessageBase

    {
        public int PrivateMessageId { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
        public int? ResponseToId { get; set; }
        public PrivateMessage ResposeTo { get; set; }
        public ICollection<PrivateMessage> Responses { get; set; }

    }
}
