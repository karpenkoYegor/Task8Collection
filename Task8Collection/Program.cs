using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Task8Collection.Data;
using Task8Collection.Data.Entities;
using Task8Collection.Data.Repository.Intefaces;
using Task8Collection.Data.Repository;
using Task8Collection.Hubs;
using Microsoft.AspNetCore.SignalR;
using Task8Collection;
using Task8Collection.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization(options => {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    })
    .AddViewLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("ru")
    };

    options.DefaultRequestCulture = new RequestCulture("ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connection);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentity<User, IdentityRole>(options => { 
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;

        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

        options.User.RequireUniqueEmail = true;
    }).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        policy => policy.RequireClaim(ClaimTypes.Role,
            "Admin"));
});
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<ISearchEngine<Item>, ItemSearchEngine>();
builder.Services.AddTransient<DataSeeder>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<CommentHub>("/hubs/comment");
if (args.Length == 1 && args[0] == "/seed")
{
    SeedData(app);
}

app.Run();

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.Seed();
    }
}