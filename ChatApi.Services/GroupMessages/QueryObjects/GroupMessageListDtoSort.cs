using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.GroupMessages.QueryObjects
{
    public static class GroupMessageListDtoSort
    {
        public static IQueryable<GroupMessageListDto> Sort(this IQueryable<GroupMessageListDto> messages)
        {
            return messages.OrderByDescending(x => x.SendingTime);
        }
    }
}
