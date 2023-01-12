using Microsoft.EntityFrameworkCore;
using OpenID.DbContexts.Interfaces;
using OpenID.Repositories;
using OpenID.Repositories.Interfaces;
using OpenID.Resources;
using OpenID.Services;
using OpenID.Services.Interfaces;


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
	}
}
