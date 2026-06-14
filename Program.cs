using Chapeau.Repositories;
using Chapeau.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Chapeau
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<DBDummyRep>();
            builder.Services.AddScoped<IBestellingService, BestellingService>();
            builder.Services.AddScoped<IBestellingRepository, BestellingRepository>();
            builder.Services.AddSingleton<IMedewerkerRepository, DBMedewerkerRepository>();
            builder.Services.AddSingleton<IMedewerkersService, MedewerkersService>();
            builder.Services.AddSingleton<IMenusRepository, DbMenusRepository>();
            builder.Services.AddScoped<ITafelRepository, DBTafelRepository>();
            builder.Services.AddScoped<IRekeningRepository, RekeningRepository>();
            builder.Services.AddScoped<Services.IRekeningService, Services.RekeningService>();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

            });

            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Login";
                    options.AccessDeniedPath = "/Login/AccessDenied";
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
