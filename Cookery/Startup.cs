using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cookery.Data;
using Cookery.Models;
using Cookery.Services.Contracts;
using Cookery.Services.Services;
using Cookery.Web.Middlewares.MiddlewareExtensions;
using Cookery.Web.Models.Account;
using Cookery.Web.Models.Order;
using Cookery.Web.Models.Product;
using Cookery.Web.Models.Recipe;
using Cookery.Web.Models.ShoppingItems;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity;

namespace Cookery.Web
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

            services.AddDbContext<CookeryDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<CookeryUser, IdentityRole>(opt =>
                {
                    opt.SignIn.RequireConfirmedEmail = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredUniqueChars = 0;
                    opt.Password.RequiredLength = 5;
                    })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<CookeryDbContext>();

            services.AddSession(options => { options.Cookie.HttpOnly = true; });
            services.AddMvc(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapper(config =>
            {
                config.CreateMap<RegisterViewModel, CookeryUser>();
                config.CreateMap<Recipe, RecipeViewModel>()
                    .ForMember(dest => dest.Products, opt => opt.Ignore());
                config.CreateMap<RecipeViewModel, Recipe>()
                    .ForMember(dest => dest.Products, opt => opt.Ignore());

                config.CreateMap<ProductViewModel, Product>();
                config.CreateMap<OrderViewModel, Order>()
                    .ForMember(dest => dest.ShoppingItem, opt => opt.Ignore())
                    .ForMember(dest => dest.CookeryUser, opt => opt.Ignore());
                config.CreateMap<ShoppingItemViewModel, ShoppingItem>()
                    .ForMember(dest => dest.User, opt => opt.Ignore());

            });
            services.AddScoped<ICookeryAccountService, CookeryAccountService>();
            services.AddScoped<IShoppingItemService, ShoppingItemService>();
            services.AddScoped<ICookeryArticleService, CookeryArticleService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IOrderService, OrderService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSeedDataMiddleware();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
