using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.Services.GroupMessages.QueryObjects
{
    public static class GroupMessageFilter
    {
        public static IQueryable<GroupMessageListDto> Filter(this IQueryable<GroupMessageListDto> messages, int groupId, int userId)
        {
            return messages.Where(m => (m.GroupId == groupId) &&
                                 (!m.IsHidden || m.ResponseReceiverId == userId || m.SenderId == userId) &&
                                 (!m.IsInvisible || !(m.SenderId == userId)));
        }
    }
}
