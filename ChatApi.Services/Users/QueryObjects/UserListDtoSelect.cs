using ChatApi.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Services.Users.QueryObjects
{
    public static class UserListDtoSelect
    {
        public static IQueryable<UserListDto> MapToDto(this IQueryable<User> query)
        {
            return query.Select(u => new UserListDto()
            {
                UserId = u.UserId,
                UserName = u.UserName,
                ProfileImageUrl = u.ProfileImageUrl ?? ""
            });
        }
    }
}
