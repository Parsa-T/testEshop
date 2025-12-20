using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Services;
using MyEshop_Phone.Infra.Data.Context;
using MyEshop_Phone.Infra.IoC;

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


#region Services
builder.Services.AddHttpClient<ILocationServices,LocationServices>();
RegisterServices(builder.Services);
static void RegisterServices(IServiceCollection services)
{
    DependencyContainer.RegisterServices(services);
}
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

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
