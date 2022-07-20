using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Podaci.EF;
using Podaci.Entiteti;
using RS1_seminarski.Security;

namespace RS1_seminarski
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
            //services.AddControllersWithViews();

            services.AddDbContext<MyContext>();

            services.AddIdentity<KorisnickiNalog, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+/ ";
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })/*.AddXmlSerializerFormatters()*/;

            services.AddAuthorization(options =>
            {
                options.AddPolicy("KreiranjePolicy", policy => policy.RequireClaim("Kreiranje", "true"));
                options.AddPolicy("UređivanjePolicy", policy => policy.RequireClaim("Uređivanje", "true")); 
                options.AddPolicy("UređivanjePolicyAdmin", policy => policy.AddRequirements(new UpravljanjeAdminUlogomIZahtjevimaMogucnosti()));
                options.AddPolicy("BrisanjePolicy", policy => policy.RequireClaim("Brisanje", "true"));
                options.AddPolicy("BrisanjePolicyAdmin", policy => policy.AddRequirements(new UpravljanjeAdminUlogomIZahtjevimaMogucnosti()));
            });

            services.AddAuthentication()
                .AddGoogle(Options =>
                {
                    Options.ClientId = "590727358324-n9780660jq4oipeki6ctnuvejn5o29up.apps.googleusercontent.com";
                    Options.ClientSecret = "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";
                })
                .AddFacebook(Options =>
                {
                    Options.AppId = "715074092819551";
                    Options.AppSecret = "8701ecf2695ca5a8c28352f6c945b7ff";
                });

            services.AddSession();
            services.AddControllersWithViews();

            services.AddScoped<IAuthorizationHandler, MozeUredjivatiSamoOstaleAdminUlogeICuvarMogucnosti>();

            services.AddSingleton<IAuthorizationHandler, MozeUredjivatiSamoOstaleAdminUlogeICuvarMogucnosti>();
            services.AddHttpContextAccessor();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder => // CORS Policy
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
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
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("MyPolicy");

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages(); 
                //endpoints.MapControllers();
            });
        }
    }
}
