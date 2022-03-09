using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReports.Services.Interfaces
{
    public interface IDbInitializer
    {
        Task<bool> RemoveAsync(CancellationToken Cancel = default);
        Task InitializerAsync(bool RemoveBefore = false, CancellationToken Cancel = default);
    }
}
