using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.EF.Models
{
    public class GroupMessage : MessageBase
    {
        public int GroupMessageId { get; set; }
        public int GroupId { get; set; }
        public bool IsHidden { get; set; }
        public bool IsSoftDeleted { get; set; }
        public int? ResponseToId { get; set; }
        public GroupMessage ResposeTo { get; set; }
        public ICollection<GroupMessage> Responses { get; set; }
    }
}
