using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherReports.DAL.Context;
using WeatherReports.Services.Interfaces;

namespace WeatherReports.Services
{
    public class DbInitializer:IDbInitializer
    {
        private readonly WeatherReportsContext _db;

        public ILogger<DbInitializer> _Logger;

        public DbInitializer(WeatherReportsContext db,ILogger<DbInitializer>Logger)
        {
            _db = db;
            _Logger = Logger;
            
        }
        public async Task<bool> RemoveAsync(CancellationToken Cancel = default)
        {
            return await _db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false);
        }
        public async Task InitializerAsync(bool RemoveBefore = false, CancellationToken Cancel = default)
        {
            if (RemoveBefore)
                await RemoveAsync(Cancel).ConfigureAwait(false);
            await _db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
        }
        private async Task InitializeWeatherAsync(CancellationToken Cancel)
        {
            if (_db.WeatherReports.Any())
            {
                return;
            }
           
        }
    }
}
