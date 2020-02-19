using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.TwitterClone.UI.Configuration;
using A2.TwitterClone.UI.model;
using A2.TwitterClone.UI.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Maqduni.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace A2.TwitterClone.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var ravenConfig = new RavenConfig();
            Configuration.GetSection("RavenDbConfig").Bind(ravenConfig);

            services.AddRavenDbAsyncSession($"{ravenConfig.DBName};{ravenConfig.Url}");
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddRavenDbStores()
                .AddDefaultTokenProviders();

                    

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
