using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.EF;
using ChatApi.Services.Database.ModelsLoading;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.Services.Database.SetupHelpers
{
    public static class UserSetupHelpers
    {
        public static async Task<int> SeedUsersWithGroups(this ChatDbContext dbContext, string dataDirectory, string fileUsersName, string fileGroupsName)
        {
            var numUsers = await dbContext.Users.CountAsync();
            if(numUsers == 0)
            {
                var users = UsersJsonLoader.LoadUsers(dataDirectory, fileUsersName, fileGroupsName);
                dbContext.Users.AddRange(users);
                numUsers = await dbContext.SaveChangesAsync();
            }
            return numUsers;
        }
    }
}
