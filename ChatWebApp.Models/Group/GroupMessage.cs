using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWebApp.Models.Group
{
    public class GroupMessage
    {
        public int Id { get; set; }
        public int? ResponseToId { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Text { get; set; }
        public bool IsHidden { get; set; }
    }
}
