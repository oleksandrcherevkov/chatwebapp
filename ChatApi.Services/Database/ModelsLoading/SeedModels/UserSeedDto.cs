using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.Database.ModelsLoading.Models
{
    public class UserSeedDto
    {
        public string UserName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string[] Groups { get; set; }
    }
}
