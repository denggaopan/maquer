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
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddConsulConfig(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5001";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "CatalogService";
                });

            services.AddSingleton<IHttpApiFactory<IUserServiceApi>, HttpApiFactory<IUserServiceApi>>(p =>
            {
                object factory = p.GetService(typeof(IHttpContextAccessor));
                var context = ((IHttpContextAccessor)factory).HttpContext;
                var auth = context.Request.Headers["Authorization"].ToString();
                var token = !string.IsNullOrEmpty(auth) && auth.StartsWith("Bearer") ? auth.Substring(7) : "";
                return new HttpApiFactory<IUserServiceApi>().ConfigureHttpApiConfig(c =>
                {
                    c.HttpHost = new Uri("http://192.168.10.50:5000/user/");
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
