using System;
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

            // Add session data to keep data in for the current user session.
            services.AddSession();
            services.AddMemoryCache();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(8);
                options.Cookie.Name = "SessionID";
            });

            // Some anti forgery stuff needed to keep the session safe.
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
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

            // Session data
            app.UseSession();
            app.UseMvc(routes => routes.MapRoute("default", "{controller}/{action=Index}/{id?}"));

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapRazorPages());
        }
    }
}