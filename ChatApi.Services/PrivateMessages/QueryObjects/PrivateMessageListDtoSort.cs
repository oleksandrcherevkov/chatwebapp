using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.PrivateMessages.QueryObjects
{
    internal static class PrivateMessageListDtoSort
    {
        public static IQueryable<PrivateMessageListDto> Sort(this IQueryable<PrivateMessageListDto> query)
        {
            return query.OrderByDescending(m => m.SendingTime);
        }
    }
}
