using Microsoft.AspNetCore.Identity;
using OpenID.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenID.Repositories.Interfaces
{
    public interface IIdentityRepository<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserClaim : IdentityUserClaim<TKey>
        where TUserRole : IdentityUserRole<TKey>
        where TUserLogin : IdentityUserLogin<TKey>
        where TRoleClaim : IdentityRoleClaim<TKey>
        where TUserToken : IdentityUserToken<TKey>
    {
        Task<bool> ExistsUserAsync(string userId);

        

        Task<PagedList<TUser>> GetUsersAsync(string search, int page = 1, int pageSize = 10);

        
    }
}
