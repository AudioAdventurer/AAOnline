using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text;
using AudioAdventurer.AdventureBuilderServer.Console.Modules;
using AudioAdventurer.Library.Common.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using AudioAdventurer.Library.Common.Helpers;
using AudioAdventurer.Library.Common.Modules;

namespace AudioAdventurer.AdventureBuilderServer.Console
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection. Don't build or return
            // any IServiceProvider or the ConfigureContainer method
            // won't get called.
            services.AddOptions();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var config = ConfigHelper.LoadFromEnvironment("AA_StandAlone");
            
            builder.RegisterModule(
                new CommonModule(config));

            builder.RegisterModule(
                new LocalModule(config));
        }

        public IConfigurationRoot Configuration { get; private set; }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogger logger)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "text/html";

                    StringBuilder sb = new StringBuilder();

                    sb.Append("<html lang=\"en\"><body>\r\n");
                    sb.Append("ERROR!<br><br>\r\n");

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    var error = exceptionHandlerPathFeature?.Error;
                    if (error != null)
                    {
                        sb.Append($"Error Type:{error.GetType()}<br>");

                        try
                        {
                            logger.Error(error.Message);
                        }
                        catch
                        {
                            //eat all errors
                        }

                        sb.Append(error.Message);
                    }
                    else
                    {
                        logger.Error("Unknown Error");
                    }

                    sb.Append("</body></html>\r\n");
                    await context.Response.WriteAsync(sb.ToString()); // IE padding
                });
            });

            DefaultFilesOptions defaultFileOptions = new DefaultFilesOptions();
            defaultFileOptions.DefaultFileNames.Clear();
            defaultFileOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFileOptions);

            string staticFiles = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            
            StaticFileOptions staticFileOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFiles),
                RequestPath = "",
                ContentTypeProvider = new FileExtensionContentTypeProvider(),
                DefaultContentType = "application/javascript"
            };
            app.UseStaticFiles(staticFileOptions);

            app.UseRouting();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

            //app.UseMvc();

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(env.WebRootPath, "index.html"));
            });
        }
    }
}
