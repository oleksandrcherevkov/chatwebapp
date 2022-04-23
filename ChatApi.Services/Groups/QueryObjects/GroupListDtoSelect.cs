using ChatApi.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.Groups.QueryObjects
{
    public static class GroupListDtoSelect
    {
        public static IQueryable<GroupListDto> MapToListDto(this IQueryable<Group> query)
        {
            return query.Select(g => new GroupListDto()
            {
                GroupId = g.GroupId,
                GroupName = g.GroupName,
                MembersCount = g.Users.Count(),
                GroupImageUrl = g.GroupImageUrl ?? ""
            });
        }
    }
}
