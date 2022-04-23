using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatWebApp.Services.Home;
using ChatWebApp.Services.Group;
using ChatWebApp.Services.Chat;

namespace ChatWebApp
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
            services.AddControllersWithViews();

            services.AddScoped<HomeService>();
            services.AddScoped<GroupService>();
            services.AddScoped<ChatService>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{userId=1}");
                endpoints.MapControllerRoute(
                    name: "Home",
                    pattern: "{userId}",
                    defaults: new { Controller = "Home", Action = "Index" });
                endpoints.MapControllerRoute(
                    name: "Group",
                    pattern: "group/{groupId}/{userId}/{page=1}", 
                    defaults: new { Controller = "Group", Action = "Index"});
                endpoints.MapControllerRoute(
                    name: "Chat",
                    pattern: "chat/{responcerId}/{userId}/{page=1}",
                    defaults: new { Controller = "Group", Action = "Index" });
            });
        }
    }
}
