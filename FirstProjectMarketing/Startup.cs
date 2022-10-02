using FirstProjectMarketing.Models;
using FirstProjectMarketing.Models.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FirstProjectMarketing
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
               services.AddDbContext<ModelContext>(options =>
            options.UseOracle(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddAuthentication(opt => {
            //    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            // .AddJwtBearer(options =>
            // {
            //     options.TokenValidationParameters = new TokenValidationParameters
            //     {
            //         ValidateIssuer = false,
            //         ValidateAudience = false,
            //         ValidateLifetime = true,
            //         ValidateIssuerSigningKey = true,

            //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
            //     };
            // });

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(1);
            });
            
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();

            services.AddScoped<IMarketingRepository<Firstrole>, RolesDbRepository>();
            services.AddScoped<IMarketingRepository<Firstlogin>, LoginDbRepository>();
            services.AddScoped<IMarketingRepository<Firstuser>, UserDbRepository>();
            services.AddScoped<IMarketingRepository<Firststore>, StoresDbRepository>();
            services.AddScoped<IMarketingRepository<Firstcatigory>, CatigoryDbRepository>();
            services.AddScoped<IMarketingRepository<Firstproduct>, ProductDbRepository>();
            services.AddScoped<IMarketingRepository<Firstdiscount>, DiscountDbRepository>();
            services.AddScoped<IMarketingRepository<Firstorderproduct>, OrderProductRepository>();
            services.AddScoped<IMarketingRepository<Firstorder>, OrderDbRepository>();
            services.AddScoped<IMarketingRepository<Firstcart>, CartItemDbRepository>();
            services.AddScoped<IMarketingRepository<Firstcartproduct>, CartProductItemDbRepository>();
            services.AddScoped<IMarketingRepository<Firstfavorite>, FavoriteDbRepository>();
            services.AddScoped<IMarketingRepository<Firsttestimonial>, TestimonailDbRepossitory>();
            services.AddScoped<IMarketingRepository<Firsthome>, HomeDbRepository>();
            services.AddScoped<IMarketingRepository<Firsthomeimage>, BackgroundHomeDbRepository>();
            services.AddScoped<IMarketingRepository<Firstcontactu>, ContactUsDbRepository>();
            services.AddScoped<IMarketingRepository<Firstaboutu>, AboutUsDbRepository>();
            services.AddScoped<IMarketingRepository<Firstpayment>, PaymentDbRepository>();
            services.AddScoped<IMarketingRepository<Firstbank>, BankDbRepository>();
            services.AddScoped<IMarketingRepository<FirstReviewProduct>, ReviewProductRepository>();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseHttpsRedirection();
            //app.UseAuthorization();
            app.UseAuthentication();
            //app.UseCookiePolicy();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
