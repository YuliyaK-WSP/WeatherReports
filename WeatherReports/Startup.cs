using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using WeatherReports.DAL.Context;

namespace WeatherReports
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }
        public Startup()
        {
            var Builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            AppConfiguration = Builder.Build();
        }

        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
           
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog= WeatherReports.db;";
            services.AddDbContext<WeatherReportsContext>(options => options.UseSqlServer(connection));
            services.AddControllersWithViews();
        }
    }
}
