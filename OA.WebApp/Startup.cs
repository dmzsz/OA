using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using OA.WebApp.Models;
using OA.WebApp.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using OA.WebApp.Authorization;
using OA.WebApp.Controllers;
using OA.WebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace OA.WebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Add(new ServiceDescriptor(typeof(T1Context), new T1Context(Configuration.GetConnectionString("T1Context"))));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPrivilegeService, PrivilegeService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();
            services.AddSession();
            services.AddAutoMapper();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login");
                    options.LogoutPath = new PathString("/Logout");
                    //options.AccessDeniedPath = new PathString("/Error/Forbidden");
                    //options.Events = new CookieAuthenticationEvents
                    //{
                    //    OnValidatePrincipal = context =>
                    //    {
                    //       //Custom validation goes here 
                    //    }
                    //};
                });
            
            services.AddDbContext<OAContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("OAContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
            });
        }
    }
}
