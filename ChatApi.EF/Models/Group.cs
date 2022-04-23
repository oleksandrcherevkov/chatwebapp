using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.EF.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string? GroupImageUrl { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<GroupMessage> GroupMessages  { get; set; }
    }
}
