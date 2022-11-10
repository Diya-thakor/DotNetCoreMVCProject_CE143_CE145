using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using OnlineDiscussionForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineDiscussionForum
{
    public class Startup
    {

        private IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AppDbConnection")));

            services.AddControllersWithViews();
            services.AddScoped<IUserRepository, SQLUserRepository>();
            services.AddScoped<IQuestionRepository, SQLQuestionRepository>();
            services.AddScoped<IAnswerRepository, SQLAnswerRepository>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default","{controller=User}/{action=login}");
                /*
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                */
            });
        }
    }
}
