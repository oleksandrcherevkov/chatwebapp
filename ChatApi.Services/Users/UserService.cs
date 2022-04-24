using ChatApi.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.Services.Users.QueryObjects;
using Microsoft.Extensions.Logging;

namespace ChatApi.Services.Users
{
    public class UserService : ServiceErrors
    {
        private readonly ChatDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public UserService(ChatDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        /// <summary>
        /// Get all users info, that have a conversation a particular user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Query of entities with users info.</returns>
        public IQueryable<UserListDto> ListChats(int userId)
        {
            var responcersQuery = _dbContext.Users
                .AsNoTracking()
                .Where(u => u.PrivateMessages.Any(m => m.ReceiverId == userId) ||
                            u.ResPrivateMessages.Any(m => m.SenderId == userId))
                .MapToDto();
                
            return responcersQuery;
        }
        /// <summary>
        /// Get user info.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Entity with user info.</returns>
        public async Task<UserListDto> GetChatAsync(int userId)
        {
            var userQuery = _dbContext.Users
                .AsNoTracking()
                .Where(u => u.UserId == userId)
                .MapToDto();

            var user = await userQuery.SingleOrDefaultAsync();

            if (user is null)
            {
                _logger.LogWarning("User with ID {UserId} does not exist", userId);
                AddError($"User with ID {userId} does not exist", nameof(userId));
            }
                

            return user;
        }
    }
}
