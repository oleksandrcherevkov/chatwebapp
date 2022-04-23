using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.Services.GeneralOptions;

namespace ChatApi.Services.PrivateMessages
{
    public class ListPrivateMessageOptions : PagingOptions
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ResponcerId  { get; set; }
        
    }
}
