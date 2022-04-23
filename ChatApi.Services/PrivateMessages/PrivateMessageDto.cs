using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.PrivateMessages
{
    public class PrivateMessageDto
    {
        public int Id { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int ReceiverId { get; set; }
        public int? ResponseToId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }
    }
}
