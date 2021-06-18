using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Blazored.LocalStorage;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using QUANLIVANBAN.Data;

namespace QUANLIVANBAN
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddServerSideBlazor();
            services.AddSingleton<LoaivanbanData>();
            services.AddSingleton<CoquanbanhanhData>();
            services.AddSingleton<TaikhoanData>();
            services.AddSingleton<VanbandenData>();
            services.AddSingleton<VanbandiData>();
            services.AddSingleton<VanbanData>();
            services.AddSingleton<VanbancanhanData>();
            services.AddSingleton<PhongbanData>();
            services.AddSingleton<ChucvuData>();
            services.AddSingleton<CoquanData>();
            services.AddSingleton<PhanQuyenData>();
            services.AddSingleton<FileUpload>();
            services.AddSingleton<InVanBanData>();
            services.AddScoped<IFileUpload, FileUpload>();
            services.AddBlazoredLocalStorage();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                //options.ReturnUrlParameter="/index";
                options.ClaimActions.MapJsonKey("urn:google:profile", "link");
                options.ClaimActions.MapJsonKey("urn:google:image", "picture");
            });
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                facebookOptions.Events = new OAuthEvents()
                {
                    OnRemoteFailure = loginFailureHandler =>
                    {
                    var authProperties = facebookOptions.StateDataFormat.Unprotect(loginFailureHandler.Request.Query["state"]);
                    loginFailureHandler.Response.Redirect("/index");
                    loginFailureHandler.HandleResponse();
                    return Task.FromResult(0);
                    }
                };
            });
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();
            services.AddSingleton<IConfiguration>(Configuration);
           // services.AddScoped<SignInManager<IdentityUser>, SignInManager<IdentityUser>>();
            //services.AddScoped<IPrintingService, PrintingService>();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseWebSockets();

            //app.UseIdentity();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
            });
        }
    }
}