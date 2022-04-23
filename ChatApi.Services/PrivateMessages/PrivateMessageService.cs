using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.EF;
using ChatApi.EF.Models;
using ChatApi.Services.GeneralOptions;
using ChatApi.Services.PrivateMessages.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.Services.PrivateMessages
{
    /// <summary>
    /// CRUD service for PrivateMessage model.
    /// </summary>
    public class PrivateMessageService : ServiceErrors
    {
        private readonly ChatDbContext _dbContext;
        public PrivateMessageService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<PrivateMessageListDto> ListMessages(ListPrivateMessageOptions options)
        {
            
            var messages = _dbContext.PrivateMessages
                            .AsNoTracking()
                            .MapToDto()
                            .Filter(options)
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

        public async Task<PrivateMessageListDto> ReadMessageAsync(int messageId)
        {
            var message = await _dbContext.PrivateMessages
                .AsNoTracking()
                .MapToDto()
                .FirstOrDefaultAsync(m => m.Id == messageId);

            if (message is null)
                AddError($"Message {messageId} does not exist", nameof(messageId));

            return message;
        }

        private async Task<PrivateMessage> GetMessageAsync(int messageId)
        {
            var message = await _dbContext.PrivateMessages
                .FirstOrDefaultAsync(m => m.PrivateMessageId == messageId);

            if (message is null)
                AddError($"Message {messageId} does not exist", nameof(messageId));

            return message;
        }

        public async Task<int> AddMessageAsync(PrivateMessageDto newMessageDto) 
        {
            var message = new PrivateMessage()
            {
                SenderId = newMessageDto.SenderId,
                ReceiverId = newMessageDto.ReceiverId,
                ResponseToId = newMessageDto.ResponseToId,
                Text = newMessageDto.Text
            };

            _dbContext.Add(message);

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

            if(message is null)
                return 0;

            message.IsInvisiblе = true;
            _dbContext.Update(message);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteMessageAsync(int messageId)
        {
            var message = await GetMessageAsync(messageId);

            if (message is null)
                return 0;

            _dbContext.Remove(message);

            return await _dbContext.SaveChangesAsync();
        }
    }
}
