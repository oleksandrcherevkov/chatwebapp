using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.EF.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }    
        public string? ProfileImageUrl { get; set; }
        public  ICollection<Group> Groups { get; set; }

        public ICollection<PrivateMessage> PrivateMessages { get; set; }
        public ICollection<PrivateMessage> ResPrivateMessages { get; set; }
    }
}
