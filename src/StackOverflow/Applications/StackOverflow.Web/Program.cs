using Autofac;
using Autofac.Extensions.DependencyInjection;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackOverflow.DAL.Utility;
using StackOverflow.Services;
using StackOverflow.Web;
using StackOverflow.Web.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new WebModule());
    containerBuilder.RegisterModule(new ServiceModule());
});

//builder.Services.AddScoped<ISeedService, SeedService>();
builder.Services.AddScoped(x => new MsSqlSessionFactory(connectionString).OpenSession());
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

var log = LogManager.GetLogger(typeof(Program));

try
{
    var app = builder.Build();
    log.Info("Application Starting up");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
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

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=QA}/{action=Index}/{id?}");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();
}
catch(Exception ex)
{
    log.Fatal($"Exception Message: {ex.Message}\nException: {ex}\n\n");
}