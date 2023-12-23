using ShipBu;
using ShipBu.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));

    config.UseLazyLoadingProxies();

});
builder.Services.AddIdentity<AppUser, AppRole>(config =>
{
    config.Password.RequiredLength = builder.Configuration.GetValue<int>("Password:RequiredLength");
    config.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Password:RequireLowercase");
    config.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Password:RequireUppercase");
    config.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Password:RequireNonAlphanumeric");
    config.Password.RequireDigit = builder.Configuration.GetValue<bool>("Password:RequireDigit");
    config.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Password:RequiredUniqueChars");

    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    config.Lockout.MaxFailedAccessAttempts = 5;

    config.SignIn.RequireConfirmedEmail = true;

    config.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;

})
    .AddEntityFrameworkStores<AppDbContext>()
     .AddDefaultTokenProviders();
builder.Services.AddSession(options =>
{
    // TempData'nýn oturum çerezini kullanmasýný engellemek için
    options.Cookie.IsEssential = true;
});

builder.Services.AddMailKit(optionBuilder =>
{
    optionBuilder.UseMailKit(new MailKitOptions()
    {
        //get options from sercets.json
        Server = builder.Configuration.GetValue<string>("EMail:Server"),
        Port = builder.Configuration.GetValue<int>("EMail:Port"),
        SenderName = builder.Configuration.GetValue<string>("EMail:SenderName"),
        SenderEmail = builder.Configuration.GetValue<string>("EMail:SenderEmail"),
        Account = builder.Configuration.GetValue<string>("EMail:SenderEmail"),
        Password = builder.Configuration.GetValue<string>("EMail:Password"),
        Security = builder.Configuration.GetValue<bool>("EMail:SslEnable")
    });
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
app.UseLogicstic();

app.UseEndpoints(endpoints =>
{
    // Areas
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

    // ContainerId
    endpoints.MapControllerRoute(
        name: "LocationInfoContainer",
        pattern: "Home/LocationInfo/{containerid?}",
        defaults: new { controller = "Home", action = "LocationInfo" });

    // BoxId
    endpoints.MapControllerRoute(
        name: "LocationInfoBox",
        pattern: "Home/LocationInfo/{boxid?}",
        defaults: new { controller = "Home", action = "LocationInfo" });

    // PaletteId
    endpoints.MapControllerRoute(
        name: "LocationInfoPalette",
        pattern: "Home/LocationInfo/{paletteid?}",
        defaults: new { controller = "Home", action = "LocationInfo" });

    // Default
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
