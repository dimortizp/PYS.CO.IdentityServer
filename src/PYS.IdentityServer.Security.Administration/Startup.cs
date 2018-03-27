using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServerWithAspIdAndEF.Data;
using IdentityServerWithAspIdAndEF.Models;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using IdentityServerWithAspIdAndEF.Profiles;
using IdentityServerWithAspIdAndEF.Services;
using PYS.IdentityServer.Security.Administration.ConfigurationStore;
using IdentityServer4.Stores;
using IdentityServer4.AccessTokenValidation;

namespace IdentityServerWithAspIdAndEF
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Environment = environment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("IdentityServerConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ConfigurationStoreContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()

                .AddDefaultTokenProviders();

            services.AddMvc();



            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddAspNetIdentity<ApplicationUser>()
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    // options.TokenCleanupInterval = 15; // frequency in seconds to cleanup stale grants. 15 is useful during debugging
                })
                .AddResourceStore<ResourceStore>();
            //services.AddScoped<IProfileService, ProfileService>();
            //if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            //else
            //{
            //    throw new exception("need to configure key material");
            //}

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "708996912208-9m4dkjb5hscn7cjrn5u0r4tbgkbj1fko.apps.googleusercontent.com";
                    options.ClientSecret = "wdfPY6t8H8cecgjlxud__4Gh";
                });


            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IResourceStoreExtended, ResourceStore>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IClientStoreExtended, ClientStore>();

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("TokenAuthentication:SecretKey").Value));
            
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";

                options.Authority = "http://216.69.181.183/identityserver/";
                options.RequireHttpsMetadata = false;
                options.ClientId = "IdentityServerAdmin";
                options.SaveTokens = true;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}
