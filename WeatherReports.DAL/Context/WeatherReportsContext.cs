using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeatherReports.Domain.Entities;
using WeatherReports.Domain.Entities.Base;

namespace WeatherReports.DAL.Context
{
    public class WeatherReportsContext : DbContext
    {
        public DbSet<Weather> WeatherReports { get; set; }
        public WeatherReportsContext(DbContextOptions<WeatherReportsContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        public WeatherReportsContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder db)
        {
            base.OnModelCreating(db);
        }

    }
}
