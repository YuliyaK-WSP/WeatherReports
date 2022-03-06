using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherReports.Domain.Entities.Base;

namespace WeatherReports.DAL.Context
{
    public class WeatherReportsDB : DbContext
    {
        public DbSet<Weather> WeatherReports { get; set; }
        protected WeatherReportsDB(DbContextOptions<WeatherReportsDB> options):base(options)
        {

        }
    }
}
