using EventHub.DataAccess.EntityFramework.DataContext;
using EventHub.Web.Api.Configurations;
using EventHub.Web.Api.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedLockNet;
using System;
using System.Linq;

namespace EventHub.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAppDependencyInjection(Configuration);
            services.AddHttpContextAccessor();
            services.AddAppOptions(Configuration);
            services.AddAppCaching(Environment, Configuration);
            services.AddAppDataProtection(Environment, Configuration);
            services.AddAppProblemDetails(Environment);
            services.AddAppForwardedHeaders();
            services.AddAppCors();
            services.AddAppIdentity();
            services.AddAppSecurity(Configuration);
            services.AddAppMvc();
            services.AddAppSwagger(Configuration);
            services.AddAppSignalR(Environment, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            UpdateDatabase(Environment, serviceProvider);
            app.ApplicationServices.GetRequiredService<AutoMapper.IConfigurationProvider>().AssertConfigurationIsValid();

            app.UseForwardedHeaders();
            app.UseAppProblemDetails();
            if (!Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAppCors();
            app.UseAppSecurity();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/hubs/chat");
            });

            app.UseAppSwagger();
        }

        private void UpdateDatabase(IWebHostEnvironment environment, IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            if (environment.IsDevelopment())
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }
            else
            {
                var distributedLockFactory = serviceProvider.GetRequiredService<IDistributedLockFactory>();
                using (var dbUpdateLock = distributedLockFactory.CreateLock("dbUpdate", TimeSpan.FromSeconds(10)))
                {
                    if (dbUpdateLock.IsAcquired && dbContext.Database.GetPendingMigrations().Any())
                    {
                        dbContext.Database.Migrate();
                    }
                }
            }
        }
    }
}
