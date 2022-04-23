using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.EF;
using ChatApi.EF.Models;
using ChatApi.Services.GeneralOptions;
using ChatApi.Services.GroupMessages.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.Services.GroupMessages
{
    /// <summary>
    /// CRUD service for GroupMessage model.
    /// </summary>
    public class GroupMessageService : ServiceErrors
    {
        private readonly ChatDbContext _dbContext;
        public GroupMessageService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<GroupMessageListDto> ListMessages(ListGroupMessageOptions options)
        {
            var messages = _dbContext.GroupMessages.AsNoTracking()
                                                   .MapToDto()
                                                   .Filter(options.GroupId, options.UserId)
                                                   .Sort();
            options.CheckPaging(messages);
            try
            {
                messages = messages.Page(options.PageNum, options.PageSize);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                AddError(ex.Message, ex?.ParamName ?? "UNKNOWN");
            }

            return messages;
        }
        public async Task<GroupMessageListDto> ReadMessageAsync(int messageId)
        {
            var message = await _dbContext.GroupMessages
                .AsNoTracking()
                .MapToDto()
                .FirstOrDefaultAsync(m => m.Id == messageId);

            if (message is null)
                AddError($"Message with ID {messageId} does not exist", nameof(messageId));

            return message;
        }
        private async Task<GroupMessage> GetMessageAsync(int messageId)
        {
            var message = await _dbContext.GroupMessages
                .FirstOrDefaultAsync(m => m.GroupMessageId == messageId);

            if (message is null)
                AddError($"Message {messageId} does not exist", nameof(messageId));

            return message;
        }
        public async Task<int> AddMessageAsync(GroupMessageDto dto)
        {
            var newMessage = (GroupMessage)dto;

            _dbContext.Add(newMessage);

            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateMessageTextAsync(int messageId, string newText)
        {
            var message = await GetMessageAsync(messageId);

            if (message is null)
                return 0;

            message.Text = newText;
            message.IsEdited = true;
            message.LastUpdateTime = DateTime.Now;

            _dbContext.Update(message);

            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> SelfDeleteMessageAsync(int messageId)
        {
            var message = await GetMessageAsync(messageId);

            if (message is null)
                return 0;

            message.IsInvisiblе = true;
            _dbContext.Update(message);

            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> SoftDeleteMessageAsync(int messageId)
        {
            var message = await GetMessageAsync(messageId);

            if (message is null)
                return 0;

            message.IsSoftDeleted = true;
            _dbContext.Update(message);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
