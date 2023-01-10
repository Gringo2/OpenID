using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OpenID.Extensions;

namespace OpenID.Repositories.Interfaces
{
    public interface IIdentityRepository<TUser, TRole, TKey>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<bool> ExistsUserAsync(string userId);

        Task<bool> ExistsRoleAsync(string roleId);

        Task<PagedList<TUser>> GetUsersAsync(string search, int page = 1, int pageSize = 10);

        Task<PagedList<TRole>> GetRolesAsync(string search, int page = 1, int pageSize = 10);

        Task<(IdentityResult identityResult, TKey roleId)> CreateRoleAsync(TRole role);

        Task<TRole> GetRoleAsync(TKey roleId);

        Task<List<TRole>> GetRolesAsync();

        Task<(IdentityResult identityResult, TKey roleId)> UpdateRoleAsync(TRole role);

        Task<TUser> GetUserAsync(string userId);

        Task<(IdentityResult identityResult, TKey userId)> CreateUserAsync(TUser user);

        Task<(IdentityResult identityResult, TKey userId)> UpdateUserAsync(TUser user);

        Task<IdentityResult> DeleteUserAsync(string userId);

        Task<IdentityResult> CreateUserRoleAsync(string userId, string roleId);

        Task<IdentityResult> DeleteUserRoleAsync(string userId, string roleId);

    }
}