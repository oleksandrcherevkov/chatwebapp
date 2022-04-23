using ChatApi.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.GroupMessages
{
    public class GroupMessageDto
    {
        public int Id { get; set; }
        public int? ResponseToId { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Text { get; set; }
        [Required]
        public bool IsHidden { get; set; }


        public static explicit operator GroupMessage(GroupMessageDto dto) 
            => new GroupMessage() 
            {
                GroupId = dto.GroupId,
                SenderId = dto.UserId,
                Text = dto.Text,
                IsHidden = dto.IsHidden,
                ResponseToId = dto.ResponseToId
            };


    }
}
