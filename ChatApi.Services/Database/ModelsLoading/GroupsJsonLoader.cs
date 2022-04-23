using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.EF.Models;
using ChatApi.Services.Database.ModelsLoading.SeedModels;
using Newtonsoft.Json;

namespace ChatApi.Services.Database.ModelsLoading
{
    public static class GroupsJsonLoader
    {
        public static ICollection<Group> LoadGroups(string fileDir, string fileSearchString)
        {
            var filePath = JsonLoaderHelper.GetJsonFilePath(fileDir, fileSearchString);
            var jsonDecoded = JsonConvert.DeserializeObject<ICollection<GroupSeedDto>>(File.ReadAllText(filePath));

            if(jsonDecoded is null)
                return new List<Group>();

            return jsonDecoded.Select(dto => new Group()
            {
                GroupName = dto.GroupName,
                GroupImageUrl = dto.GroupImageUrl
            }).ToList();
        }
    }
}
