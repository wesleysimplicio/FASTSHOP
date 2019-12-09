using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using FASTSHOP.Api.Domain.Models;
using FASTSHOP.Api.Domain.Business;
using FASTSHOP.Api.Domain.Repositories;
using FASTSHOP.Api.Domain.Interfaces;
using NLog.Extensions.Logging;
using NLog;
using FASTSHOP.Api.Domain.Mongo;
using FASTSHOP.Api.Models;

namespace FASTSHOP.Api
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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvcCore(options =>
            {
                options.Filters.Add(typeof(ValidateModelFilter));
            });

            services.Configure<MongoConfiguration>(Configuration.GetSection("MongoDB"));
            services.AddMongoDB();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientBusiness, ClientBusiness>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<IMongoClient, MongoClient>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"))
                .AddDebug(Microsoft.Extensions.Logging.LogLevel.None)
                .AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });

            LogManager.LoadConfiguration("nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }


    }
}


