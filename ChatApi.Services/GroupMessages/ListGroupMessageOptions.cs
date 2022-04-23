using ChatApi.Services.GeneralOptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.GroupMessages
{
    public class ListGroupMessageOptions : PagingOptions
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
