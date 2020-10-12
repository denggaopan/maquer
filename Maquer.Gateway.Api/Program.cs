using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;


namespace Maquer.Gateway.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                 .UseStartup<Startup>()
             .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 config.AddJsonFile("ocelot.json", false, true);
             });
            //.ConfigureServices(services =>
            //{
            //    var authenticationProviderKey = "OcelotKeykkkkkkkkkk";
            //    var identityServerOptions = new IdentityServerOptions();
            //    Configuration.Bind("IdentityServerOptions", identityServerOptions);
            //    services.AddAuthentication(identityServerOptions.IdentityScheme)
            //        .AddIdentityServerAuthentication(authenticationProviderKey, options =>
            //        {
            //            options.RequireHttpsMetadata = false; //是否启用https
            //        options.Authority = $"http://{identityServerOptions.ServerIP}:{identityServerOptions.ServerPort}";//配置授权认证的地址
            //        options.ApiName = identityServerOptions.ResourceName; //资源名称，跟认证服务中注册的资源列表名称中的apiResource一致
            //        options.SupportedTokens = SupportedTokens.Both;
            //        }
            //        );

        //    services.AddOcelot()
        //        .AddConsul()
        //        .AddPolly();
        //})
        //.Configure(app =>
        //{
        //    app.UseOcelot().Wait();
        //});
    }
}
