using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.PrivateMessages.QueryObjects
{
    internal static  class PrivateMessageListDtoFilter
    {
        public static IQueryable<PrivateMessageListDto> Filter(this IQueryable<PrivateMessageListDto> query, ListPrivateMessageOptions options)
        {
            return query.Where(m =>
            (m.SenderId == options.UserId && m.ReceiverId == options.ResponcerId && !m.IsInvisiblе) ||
            (m.SenderId == options.ResponcerId && m.ReceiverId == options.UserId)); 
        }
    }
}
