using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.EF.Models;

namespace ChatApi.Services.PrivateMessages.QueryObjects
{
    internal static class PrivateMessageListDtoSelect
    {
        public static IQueryable<PrivateMessageListDto> MapToDto (this IQueryable<PrivateMessage> query)
        {
            return query.Select(m => new PrivateMessageListDto()
            {
                Id = m.PrivateMessageId,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                ResponseToId = m.ResponseToId,
                SenderName = m.Sender.UserName,
                Text = m.Text,
                IsInvisiblе = m.IsInvisiblе,
                SendingTime = m.SendingTime,
                LastUpdateTime = m.IsEdited ? m.LastUpdateTime : m.SendingTime,
                IsEdited = m.IsEdited
            });
        }
    }
}
