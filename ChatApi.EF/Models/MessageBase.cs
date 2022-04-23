using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.EF.Models
{
    public class MessageBase
    {
        public int SenderId { get; set; }
        public bool IsInvisiblе { get; set; }
        [StringLength(1000)]
        public string Text { get; set; }
        public DateTime SendingTime { get; set; }  = DateTime.Now;
        public bool IsEdited { get; set; }
        public DateTime LastUpdateTime { get; set; }
        

        public User Sender { get; set; }
        
    }
}
