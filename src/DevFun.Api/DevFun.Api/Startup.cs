using System;
using _4tecture.DependencyInjection.AspNet;
using _4tecture.DependencyInjection.AutofacAdapter;
using DevFun.Logic.Modularity;
using DevFun.Storage.Modularity;
using DevFun.Storage.Storages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DevFun.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.AddConfiguration(Configuration.GetSection("Logging")).AddConsole());
            services.AddLogging(builder => builder.AddDebug());

            // Add framework services.
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<DevFunStorage>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DevFunDatabase")));

            // init modules
            var moduleCatalog = services.InitializeModuleCatalog();
            moduleCatalog.AddDevFunLogicModule();
            moduleCatalog.AddDevFunStorageModule();
            services.AddModulesFromConfiguration(Configuration, moduleCatalog);

            services.AddSingleton<IConfiguration>(Configuration);

            return services.SetupAutofac();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc();
        }
    }
}