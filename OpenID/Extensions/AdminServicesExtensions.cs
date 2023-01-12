using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenId.Identity.Resources;
using OpenID.DbContexts.Interfaces;
using OpenID.Dtos.Identity;
using OpenID.Identity.Resources;
using OpenID.Repositories;
using OpenID.Repositories.Interfaces;
using OpenID.Resources;
using OpenID.Services;
using OpenID.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class AdminServicesExtensions
	{
		public static IServiceCollection AddAdminServices<TAdminDbContext>(
			this IServiceCollection services)
			where TAdminDbContext : DbContext, IAdminPersistedGrantDbContext, IAdminConfigurationDbContext
		{

			return services.AddAdminServices<TAdminDbContext, TAdminDbContext>();
		}

		public static IServiceCollection AddAdminServices<TConfigurationDbContext, TPersistedGrantDbContext>(this IServiceCollection services)
			where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
			where TConfigurationDbContext : DbContext, IAdminConfigurationDbContext
		{
			//Repositories
			services.AddTransient<IClientRepository, ClientRepository<TConfigurationDbContext>>();

			
			//Services
			services.AddTransient<IClientService, ClientService>();
			

			//Resources
			
			services.AddScoped<IClientServiceResources, ClientServiceResources>();
			

			return services;
		}

        public static IServiceCollection AddAdminAspNetIdentityServices<TIdentityDbContext, TPersistedGrantDbContext, TUserDto, TRoleDto, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken,
                    TUsersDto, TRolesDto, TUserRolesDto, TUserClaimsDto,
                    TUserProviderDto, TUserProvidersDto, TUserChangePasswordDto, TRoleClaimsDto, TUserClaimDto, TRoleClaimDto>(
                        this IServiceCollection services)
            where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
            where TIdentityDbContext : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
            where TUserDto : UserDto<TKey>
            where TUser : IdentityUser<TKey>
            where TRole : IdentityRole<TKey>
            where TKey : IEquatable<TKey>
            where TUserClaim : IdentityUserClaim<TKey>
            where TUserRole : IdentityUserRole<TKey>
            where TUserLogin : IdentityUserLogin<TKey>
            where TRoleClaim : IdentityRoleClaim<TKey>
            where TUserToken : IdentityUserToken<TKey>
            where TRoleDto : RoleDto<TKey>
            where TUsersDto : UsersDto<TUserDto, TKey>
            where TRolesDto : RolesDto<TRoleDto, TKey>
            where TUserRolesDto : UserRolesDto<TRoleDto, TKey>
            where TUserClaimsDto : UserClaimsDto<TUserClaimDto, TKey>
            where TUserProviderDto : UserProviderDto<TKey>
            where TUserProvidersDto : UserProvidersDto<TUserProviderDto, TKey>
            where TUserChangePasswordDto : UserChangePasswordDto<TKey>
            where TRoleClaimsDto : RoleClaimsDto<TRoleClaimDto, TKey>
            where TUserClaimDto : UserClaimDto<TKey>
            where TRoleClaimDto : RoleClaimDto<TKey>
        {
            //Repositories
            services.AddTransient<IIdentityRepository<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>, IdentityRepository<TIdentityDbContext, TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>>();

            return services;
        }
    }
}
