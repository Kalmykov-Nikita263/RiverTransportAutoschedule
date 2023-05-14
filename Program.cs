using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RiverTransportAutoschedule.Domain;
using RiverTransportAutoschedule.Domain.Entities;
using RiverTransportAutoschedule.Domain.Repository.Abstractions;
using RiverTransportAutoschedule.Domain.Repository.EntityFramework;
using RiverTransportAutoschedule.Infrastructure;

namespace RiverTransportAutoschedule;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<IRiverTransportRepository, EFRiverTransportRepository>();
        builder.Services.AddTransient<IScheduleRepository, EFScheduleRepository>();
        builder.Services.AddTransient<IRiverPortRepository, EFRiverPortRepository>();
        builder.Services.AddTransient<DataManager>();
        builder.Services.AddTransient<ScheduleGenerator>();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDefaultConnection"));
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(30);

            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.HttpOnly = true;
        });

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCookiePolicy();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.Run();
    }
}