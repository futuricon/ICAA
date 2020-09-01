using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartBreadcrumbs.Extensions;
using ICAA.Models;

namespace ICAA
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
            services.Configure<CookiePolicyOptions>(options =>
            {

                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddBreadcrumbs(GetType().Assembly);

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration["Data:ICAAMain:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(
                Configuration["Data:ICAAMain:ConnectionString"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IBlogRepository, EFBlogRepository>();
            services.AddTransient<IStaffRepository, EFStaffRepository>();
            services.AddTransient<IGalleryRepository, EFGalleryRepository>();
            services.AddTransient<ICertRepository, EFCertRepository>();
            services.AddTransient<ITagRepository, EFTagRepository>();
            services.AddMvc()
                .AddViewLocalization(opts => { opts.ResourcesPath = "Resources"; })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<RequestLocalizationOptions>(opts => {
                var supportedCultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("ru"),
                    new CultureInfo("uz"),
                  };

                opts.DefaultRequestCulture = new RequestCulture("en");
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: null,
                //    template: "Gallery/Page{imgPage}",
                //    defaults: new { controller = "Gallery", action = "Index" });
                routes.MapRoute(
                    name: null,
                    template: "Staff/{id}",
                    defaults: new { controller = "Staff", action = "Index" });
                routes.MapRoute(
                    name: "pagination",
                    template: "News/Page{blogPage}",
                    defaults: new { controller = "News", action = "Index" });

                routes.MapRoute(
                    name: "blogPage",
                    template: "News/{id}",
                    defaults: new { controller = "News", action = "News" });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //SeedData.EnsurePopulated(app);
            //SeedDataToStaff.EnsurePopulatedStaff(app);

            //IdentitySeedData.EnsurePopulated(app);
            //IdentSeedData.EnsurePopulatedIdentRole(app);
        }
        
    }
}
