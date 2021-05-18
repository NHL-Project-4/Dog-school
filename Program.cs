using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
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
            // Add razor pages component to project
            services.AddRazorPages();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            // Set ASP.NET preferences
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // Add endpoints
            app.UseEndpoints(endpoints => endpoints.MapRazorPages());
        }
    }
}