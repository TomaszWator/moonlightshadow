using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MoonlightShadow.Models;
using MoonlightShadow.Services;
using Microsoft.AspNetCore.Authentication;
using WebApi.Services;

namespace MoonlightShadow
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
            services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = false;
            });

            services.AddAuthentication("CookieAuth")
                    .AddCookie("CookieAuth", config =>
                    {
                        config.Cookie.Name = "Identity.Cookie";
                        config.LoginPath = "/Login/Index";
                    });

            // services.AddWebOptimizer(pipeline =>
            // {
            //     pipeline.MinifyCssFiles("lib/bootstrap/dist/css/bootstrap.css", "css/*.css");
            // });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<MailSettings>(
                Configuration.GetSection(nameof(MailSettings)));

            services.AddSingleton<IMailSettings>(sp => sp.GetRequiredService<IOptions<MailSettings>>().Value);

            services.Configure<DatabaseSettings>(
                Configuration.GetSection(nameof(DatabaseSettings)));

            IServiceCollection serviceCollection = services
                .AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            
            services.AddTransient<MailSenderService>();

            services.AddSingleton<UserService>();

            services.AddSingleton<ProductService>();
            
            services.AddSingleton<CameraService>();
            
            services.AddSingleton<LaptopService>();
            
            services.AddSingleton<GameService>();
            
            services.AddSingleton<PhoneService>();
            
            services.AddSingleton<ContactService>();
            
            services.AddSingleton<OrderService>();

            services.AddSingleton<TransactionService>();

            services.AddSingleton<SessionService>();

            services.AddHttpContextAccessor();

            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseWebOptimizer();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/");
            });
        }
    }
}
