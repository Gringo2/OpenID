using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenID.Extensions;
using OpenID.Repositories.Interfaces;

namespace OpenID.Repositories
{
    public class IdentityRepository<TIdentityDbContext, TUser, TRole, TKey>
        : IIdentityRepository<TUser, TRole, TKey>
        where TIdentityDbContext : IdentityDbContext<TUser, TRole, TKey>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly TIdentityDbContext DbContext;
        protected readonly UserManager<TUser> UserManager;
        protected readonly RoleManager<TRole> RoleManager;
        protected readonly IMapper Mapper;

        public bool AutoSaveChanges { get; set; } = true;

        public IdentityRepository(TIdentityDbContext dbContext,
            UserManager<TUser> userManager,
            RoleManager<TRole> roleManager,
            IMapper mapper)
        {
            DbContext = dbContext;
            UserManager = userManager;
            RoleManager = roleManager;
            Mapper = mapper;
        }

        public virtual TKey ConvertKeyFromString(string id)
        {
            if (id == null)
            {
                return default;
            }
            return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
        }

        public virtual Task<bool> ExistsUserAsync(string userId)
        {
            var id = ConvertKeyFromString(userId);

            return UserManager.Users.AnyAsync(x => x.Id.Equals(id));
        }

        public virtual Task<bool> ExistsRoleAsync(string roleId)
        {
            var id = ConvertKeyFromString(roleId);

            return RoleManager.Roles.AnyAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<PagedList<TUser>> GetUsersAsync(string search, int page = 1, int pageSize = 10)
        {
            var pagedList = new PagedList<TUser>();
            Expression<Func<TUser, bool>> searchCondition = x => x.UserName.Contains(search) || x.Email.Contains(search);

            var users = await UserManager.Users.WhereIf(!string.IsNullOrEmpty(search), searchCondition).PageBy(x => x.Id, page, pageSize).ToListAsync();

            pagedList.Data.AddRange(users);

            pagedList.TotalCount = await UserManager.Users.WhereIf(!string.IsNullOrEmpty(search), searchCondition).CountAsync();
            pagedList.PageSize = pageSize;

            return pagedList;
        }

        public virtual Task<List<TRole>> GetRolesAsync()
        {
            return RoleManager.Roles.ToListAsync();
        }

        public virtual async Task<PagedList<TRole>> GetRolesAsync(string search, int page = 1, int pageSize = 10)
        {
            var pagedList = new PagedList<TRole>();

            Expression<Func<TRole, bool>> searchCondition = x => x.Name.Contains(search);
            var roles = await RoleManager.Roles.WhereIf(!string.IsNullOrEmpty(search), searchCondition).PageBy(x => x.Id, page, pageSize).ToListAsync();

            pagedList.Data.AddRange(roles);
            pagedList.TotalCount = await RoleManager.Roles.WhereIf(!string.IsNullOrEmpty(search), searchCondition).CountAsync();
            pagedList.PageSize = pageSize;

            return pagedList;
        }

        public virtual Task<TRole> GetRoleAsync(TKey roleId)
        {
            return RoleManager.Roles.Where(x => x.Id.Equals(roleId)).SingleOrDefaultAsync();
        }

        public virtual async Task<(IdentityResult identityResult, TKey roleId)> CreateRoleAsync(TRole role)
        {
            var identityResult = await RoleManager.CreateAsync(role);

            return (identityResult, role.Id);
        }

        public virtual async Task<(IdentityResult identityResult, TKey roleId)> UpdateRoleAsync(TRole role)
        {
            var existingRole = await RoleManager.FindByIdAsync(role.Id.ToString());
            Mapper.Map(role, existingRole);
            var identityResult = await RoleManager.UpdateAsync(existingRole);

            return (identityResult, role.Id);
        }

        public virtual async Task<IdentityResult> DeleteRoleAsync(TRole role)
        {
            var thisRole = await RoleManager.FindByIdAsync(role.Id.ToString());

            return await RoleManager.DeleteAsync(thisRole);
        }

        public virtual Task<TUser> GetUserAsync(string userId)
        {
            return UserManager.FindByIdAsync(userId);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>This method returns identity result and new user id</returns>
        public virtual async Task<(IdentityResult identityResult, TKey userId)> CreateUserAsync(TUser user)
        {
            var identityResult = await UserManager.CreateAsync(user);

            return (identityResult, user.Id);
        }

        public virtual async Task<(IdentityResult identityResult, TKey userId)> UpdateUserAsync(TUser user)
        {
            var userIdentity = await UserManager.FindByIdAsync(user.Id.ToString());
            Mapper.Map(user, userIdentity);
            var identityResult = await UserManager.UpdateAsync(userIdentity);

            return (identityResult, user.Id);
        }

        public virtual async Task<IdentityResult> CreateUserRoleAsync(string userId, string roleId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var selectRole = await RoleManager.FindByIdAsync(roleId);

            return await UserManager.AddToRoleAsync(user, selectRole.Name);
        }

        public virtual async Task<IdentityResult> DeleteUserRoleAsync(string userId, string roleId)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            var user = await UserManager.FindByIdAsync(userId);

            return await UserManager.RemoveFromRoleAsync(user, role.Name);
        }

        public virtual async Task<List<UserLoginInfo>> GetUserProvidersAsync(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var userLoginInfos = await UserManager.GetLoginsAsync(user);

            return userLoginInfos.ToList();
        }

        public virtual async Task<IdentityResult> UserChangePasswordAsync(string userId, string password)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var token = await UserManager.GeneratePasswordResetTokenAsync(user);

            return await UserManager.ResetPasswordAsync(user, token, password);
        }

        public virtual async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var userIdentity = await UserManager.FindByIdAsync(userId);

            return await UserManager.DeleteAsync(userIdentity);
        }

        protected virtual async Task<int> AutoSaveChangesAsync()
        {
            return AutoSaveChanges ? await DbContext.SaveChangesAsync() : (int)SavedStatus.WillBeSavedExplicitly;
        }

        public virtual async Task<int> SaveAllChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}