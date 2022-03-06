using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherReports.DAL.Context;
using WebStore.Infrastructure.Conventions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var services = builder.Services;
services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});
services.AddDbContext<WeatherReportsDB>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
