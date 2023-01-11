// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using OpenID.Configuration.Database;
using OpenID.DbContexts.Interfaces;
using OpenID.DbContexts.Shared;
using OpenID.Entities.Identity;
using OpenID.Models;
using OpenID.Repositories;
using OpenID.Services;
using OpenID.Services.Interfaces;
using OpenID.Shared.Dtos;
using OpenID.Shared.Dtos.Identity;
using Skoruba.AuditLogging.EntityFramework.Entities;
using System.Linq;
using System.Reflection;

namespace OpenID
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();
			
			var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();
			
			var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            }).AddAspNetIdentity<ApplicationUser>()
                 // this adds the config data from DB (clients, resources, CORS)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString,
                     sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                });

			

			// not recommended for production - you need to store your key material somewhere secure
			builder.AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5001/signin-google
                    options.ClientId = "copy client ID from Google here";
                    options.ClientSecret = "copy client secret from Google here";
                });
            services.AddAdminServices<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext>();
			// Adds the IdentityServer4 Admin UI with custom options.
			services.AddIdentityServer4AdminUI<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext,
			AdminLogDbContext, AdminAuditLogDbContext, AuditLog, IdentityServerDataProtectionDbContext,
				UserIdentity, UserIdentityRole, UserIdentityUserClaim, UserIdentityUserRole,
				UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken, string,
				IdentityUserDto, IdentityRoleDto, IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
				IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
				IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>(ConfigureUIOptions);

		}

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            // this will do the initial DB population
            //InitializeDatabase(app);

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapAreaControllerRoute(
                name: "areas",
                areaName: "Admin",
                pattern: "{area:exists}/{controller=Test}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

		public virtual void ConfigureUIOptions(IdentityServer4AdminUIOptions options)
		{
			// Applies configuration from appsettings.
			options.BindConfiguration(Configuration);
			if (Environment.IsDevelopment())
			{
				options.Security.UseDeveloperExceptionPage = true;
			}
			else
			{
				options.Security.UseHsts = true;
			}

			// Set migration assembly for application of db migrations
			var migrationsAssembly = MigrationAssemblyConfiguration.GetMigrationAssemblyByProvider(options.DatabaseProvider);
			options.DatabaseMigrations.SetMigrationsAssemblies(migrationsAssembly);

			// Use production DbContexts and auth services.
			options.Testing.IsStaging = false;
		}

		//private void InitializeDatabase(IApplicationBuilder app)
		//{
		//    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
		//    {
		//        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

		//        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
		//        context.Database.Migrate();
		//        if (!context.Clients.Any())
		//        {
		//            foreach (var client in Config.Clients)
		//            {
		//                context.Clients.Add(client.ToEntity());
		//            }
		//            context.SaveChanges();
		//        }

		//        if (!context.IdentityResources.Any())
		//        {
		//            foreach (var resource in Config.IdentityResources)
		//            {
		//                context.IdentityResources.Add(resource.ToEntity());
		//            }
		//            context.SaveChanges();
		//        }

		//        if (!context.ApiScopes.Any())
		//        {
		//            foreach (var resource in Config.ApiScopes)
		//            {
		//                context.ApiScopes.Add(resource.ToEntity());
		//            }
		//            context.SaveChanges();
		//        }
		//    }
		//}
	}
}
