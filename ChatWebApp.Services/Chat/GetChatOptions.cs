using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp.Services.Chat
{
    public class GetChatOptions
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ResponcerId { get; set; }
        [Required]
        public int PageNum { get; set; }
        public int PageSize { get; set; } = 20;
    }
}
