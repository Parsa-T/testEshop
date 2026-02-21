using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyEshop_Phone.Application.Common.Interfaces;
using MyEshop_Phone.Application.Common.setting;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Services;
using MyEshop_Phone.Infra.Data.Context;
using MyEshop_Phone.Infra.IoC;
using QuestPDF.Drawing;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

#region Database
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EShop_PhoneDb"));
});
#endregion

#region PDF
QuestPDF.Settings.License = LicenseType.Community;
FontManager.RegisterFont(File.OpenRead("wwwroot/fonts/Vazir.ttf"));
#endregion

#region Services
builder.Services.AddHttpClient<SendSmsSercives>();
builder.Services.AddHttpClient<ILocationServices, LocationServices>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(GetUserProfileQuery).Assembly));
builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<MyDbContext>());
RegisterServices(builder.Services,builder.Configuration);
static void RegisterServices(IServiceCollection services, IConfiguration configuration)
{
    DependencyContainer.RegisterServices(services,configuration);
}
#endregion


#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Login";
        options.LogoutPath = "/Account/Loguot";
        options.ExpireTimeSpan = TimeSpan.FromDays(10);
    });
#endregion

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

app.MapRazorPages();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
