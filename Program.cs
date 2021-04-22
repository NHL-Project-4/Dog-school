using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Dog_school
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure host builder
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(hostBuilder => hostBuilder.UseStartup<Program>());

            // Build ASP.NET host
            var host = builder.Build();
            host.Run();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            // Add MVC components to project
            services.AddControllersWithViews();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseExceptionHandler("/Error");

            // Set ASP.NET preferences
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Add endpoints
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}