using ChatApi.EF.Models;
using ChatApi.Services.Users;
using ChatApi.Services.Users.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.Groups.QueryObjects
{
    public static class GroupGetDtoSelect
    {
        public static IQueryable<GroupGetDto> MapToGetDto(this IQueryable<Group> query)
        {
            return query.Select(g => new GroupGetDto()
            {
                GroupId = g.GroupId,
                GroupName = g.GroupName,
                MembersCount = g.Users.Count(),
                GroupImageUrl = g.GroupImageUrl ?? "",
                Members = g.Users.Select(u => new UserListDto() 
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    ProfileImageUrl = u.ProfileImageUrl ?? ""
                }).OrderBy(g => g.UserName).ToList()
            });
        }
    }
}
