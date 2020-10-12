using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maquer.Common;
using Maquer.Domain.Catalog.Entities;
using Maquer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using WebApiClient;
using Maquer.UserService.ApiClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Maquer.CatalogService.Api
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
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddConsulConfig(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["Identity:Host"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = "CatalogService";
                });

            services.AddSingleton<IHttpApiFactory<IUserServiceApi>, HttpApiFactory<IUserServiceApi>>(p =>
            {
                var token = string.Empty;
                try
                {
                    var factory = p.GetService(typeof(IHttpContextAccessor));
                    var context = ((IHttpContextAccessor)factory).HttpContext;
                    var auth = (string)context.Request.Headers["Authorization"];
                    token = (!string.IsNullOrEmpty(auth) && auth.StartsWith("Bearer")) ? auth.Substring(7) : "";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"==>token ex:{ex.ToString()}");
                }

                return new HttpApiFactory<IUserServiceApi>().ConfigureHttpApiConfig(c =>
                {
                    c.HttpHost = new Uri($"{Configuration["Gateway:Host"]}/{Configuration["Services:User:ShortName"]}/");
                    c.LoggerFactory = p.GetRequiredService<ILoggerFactory>();
                    c.GlobalFilters.Add(new ApiTokenFilter(token));
                });
            });
            services.AddTransient<IUserServiceApi>(p =>
            {
                var factory = p.GetRequiredService<IHttpApiFactory<IUserServiceApi>>();
                return factory.CreateHttpApi();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContext db)
        {
            if (db.Database.GetPendingMigrations().Any())
            {
                db.Database.Migrate();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConsul(Configuration);

            app.UseRouting();

            app.UseAuthentication();//认证
            app.UseAuthorization();//授权

            //app.UseMiddleware<CustomAuthMiddleware>();//自定义授权

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
