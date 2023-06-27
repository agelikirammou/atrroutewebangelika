using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArtRouteWeb
{
    public class Program
    {
        public static IConfiguration Configuration { get; private set; }
        private static WebApplicationBuilder CreateAppBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json", false, true)
                                 .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true, true)
                                 .AddEnvironmentVariables();
            Configuration = builder.Configuration;

            return builder;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews()
                            .AddDataAnnotationsLocalization();
            builder.Services.AddRazorPages();
        }

        public static void Main(string[] args)
        {
            var builder = CreateAppBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "home",
                pattern: "/{action=Index}",
                defaults: new { controller = "Home" });
            app.MapRazorPages();
            app.MapControllers();
            app.Run();


            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseApplicationInsights()
                .Build();






            host.Run();
        }
    }
}
