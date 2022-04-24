using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.EF;
using ChatApi.Services.Groups.QueryObjects;
using ChatApi.Services.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChatApi.Services.Groups
{
    public class GroupService : ServiceErrors
    {
        private readonly ChatDbContext _dbContext;
        private readonly ILogger<GroupService> _logger;

        public GroupService(ChatDbContext dbContext, ILogger<GroupService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        /// <summary>
        /// Get all groups info of particular user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Query of entities with groups info.</returns>
        public IQueryable<GroupListDto> ListGroups(int userId)
        {
            var groupsQuery = _dbContext.Groups
                .AsNoTracking()
                .Where(g => g.Users.Any(u => u.UserId == userId))
                .MapToListDto();

            return groupsQuery;
        }
        /// <summary>
        /// Get a group by ID.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns>Entity with groups info and all members info.</returns>
        public async Task<GroupGetDto> GetGroupAsync(int groupId)
        {
            var groupQuery = _dbContext.Groups
                .AsNoTracking()
                .Where(g => g.GroupId == groupId)
                .MapToGetDto();

            var group = await groupQuery.FirstOrDefaultAsync();

            if (group is null)
            {
                _logger.LogWarning("Group with ID {GroupId} does not exist.", groupId);
                AddError($"Group with ID {groupId} does not exist.", nameof(groupId));
            }
                

            return group;
        }
    }
}
