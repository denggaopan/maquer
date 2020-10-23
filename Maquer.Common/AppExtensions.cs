using System;
using System.Linq;
using System.Net.Http;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Maquer.Common
{
    public static class AppExtensions
    {

        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = configuration.GetValue<string>("Consul:Host");
                consulConfig.Address = new Uri(address);
            }));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            if (!(app.Properties["server.Features"] is FeatureCollection features)) return app;

            //var addresses = features.Get<IServerAddressesFeature>();
            //var address = addresses.Addresses.First();
            var address = configuration["Service:Url"];

            //var address = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Host.ToString();
            Console.WriteLine($"address={address}");

            var serviceName = configuration["Service:Name"];
            var uri = new Uri(address);
            var registration = new AgentServiceRegistration()
            {
                ID = $"{serviceName}-{Guid.NewGuid().ToString("N")}", //ID = $"{serviceName}-{uri.Port}",
                Name = serviceName,
                Address = uri.Host,
                Port = uri.Port,
                Tags = new[] { $"urlprefix-/{serviceName}" },//添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(10),//健康检查时间间隔
                    HTTP = $"{address}/api/health",//健康检查地址
                    Timeout = TimeSpan.FromSeconds(5),
                    //TLSSkipVerify = false,
                }

            };

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            });

            return app;
        }
    }
}
