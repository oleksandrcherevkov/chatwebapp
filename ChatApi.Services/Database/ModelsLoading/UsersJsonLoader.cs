using ChatApi.EF.Models;
using ChatApi.Services.Database.ModelsLoading.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.Database.ModelsLoading
{
    public static class UsersJsonLoader
    {
        public static IEnumerable<User> LoadUsers(string fileDir, string fileUsersName, string fileGroupsName)
        {
            var filePath = JsonLoaderHelper.GetJsonFilePath(fileDir, fileUsersName);
            var jsonDecoded = JsonConvert.DeserializeObject<ICollection<UserSeedDto>>(File.ReadAllText(filePath));

            var groupsDistinct = GroupsJsonLoader.LoadGroups(fileDir, fileGroupsName).ToDictionary(g => g.GroupName);

            if(jsonDecoded is null)
                return new List<User>();

            return jsonDecoded.Select(dto => CreateUserWithGroups(dto, groupsDistinct)).ToList();
        }

        private static User CreateUserWithGroups(UserSeedDto dto, Dictionary<string, Group> groupsDict)
        {
            var user = new User() { 
                UserName = dto.UserName,
                ProfileImageUrl = dto.ProfileImageUrl
            };

            user.Groups = new List<Group>();

            foreach(var group in dto.Groups)
                user.Groups.Add(groupsDict[group]);

            return user;
        }
    }
}
