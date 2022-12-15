using System.IO;
using System.Net;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AudioAdventurer.AdventureBuilderServer.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webHostBuilder => {
                    webHostBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseKestrel()
                        .ConfigureServices(services =>
                        {
                            services.AddCors();

                            services.AddMvc()
                                .AddControllersAsServices();

                            services.AddAuthentication();
                            services.AddAuthorization();
                        })
                        .UseStartup<Startup>()
                        .ConfigureKestrel((context, options) =>
                        {
                            // Set properties and call methods on options
                            options.Listen(IPAddress.Any, 8000, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                                //listenOptions.UseHttps("testCert.pfx", "testPassword");
                            });
                        });
                })
                .Build();

            host.Run();
        }
    }
}