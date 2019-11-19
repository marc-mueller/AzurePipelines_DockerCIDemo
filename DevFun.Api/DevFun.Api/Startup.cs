using System;
using _4tecture.AspNetCoreExtensions.Middleware;
using _4tecture.AspNetCoreExtensions.Swagger;
using _4tecture.DependencyInjection.AspNet;
using DevFun.Logic.Modularity;
using DevFun.Storage.Modularity;
using DevFun.Storage.Storages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevFun.Api
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "needed by design")]
    public class Startup : _4tecture.DependencyInjection.AutofacAdapter.StartupBase
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddHealthChecks();

            // Add framework services.
            services.AddCors();
            services.AddControllers();

            services.AddApiVersiongingAndSwagger("DevFun API", "'v'VVV");

            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<DevFunStorage>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DevFunDatabase")));

            // init modules
            var moduleCatalog = services.InitializeModuleCatalog();
            moduleCatalog.AddDevFunLogicModule();
            moduleCatalog.AddDevFunStorageModule();
            services.AddModulesFromConfiguration(Configuration, moduleCatalog);

            services.AddSingleton<IConfiguration>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                // reporting
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();

                app.EnsureSqlTablesAreCreated<DevFunStorage>();
            }

            app.UseCustomExceptionHandling();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseSwaggerAndSwaggerUi("DevFun API");

            //app.UseWelcomePage();
        }
    }
}