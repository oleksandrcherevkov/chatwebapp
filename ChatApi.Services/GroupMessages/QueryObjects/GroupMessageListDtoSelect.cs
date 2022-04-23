using ChatApi.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.GroupMessages.QueryObjects
{
    public static class GroupMessageListDtoSelect
    {
        public static IQueryable<GroupMessageListDto> MapToDto(this IQueryable<GroupMessage> messages)
        {
            return messages.Select(m => new GroupMessageListDto()
            {
                Id = m.GroupMessageId,
                GroupId = m.GroupId,
                SenderId = m.SenderId,
                ResponseToMessageId = m.ResponseToId,
                ResponseReceiverId = m.ResposeTo.SenderId,
                ResponseReceiverName = m.ResposeTo.Sender.UserName,
                SenderName = m.Sender.UserName,
                Text = m.Text,
                SendingTime = m.SendingTime,
                LastUpdateTime = m.IsEdited ? m.LastUpdateTime : m.SendingTime,
                IsEdited = m.IsEdited,
                IsInvisible = m.IsInvisiblе,
                IsHidden = m.IsHidden
            });;
        }
    }
}
