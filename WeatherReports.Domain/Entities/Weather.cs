using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherReports.Domain.Entities.Base.Interfase;

namespace WeatherReports.Domain.Entities.Base
{
    [Table("Weather")]
    public class Weather :Entity
    {
        public string Data { get; set; }
        public string Time { get; set; }
        public string Temperature { get; set; }
        
        public string Humidity { get; set; }
        public string Td { get; set; }
        public string AtmosphericPressure { get; set; }
        public string WindDirection { get; set; }
        public string WindSpeed { get; set; }
        public string Cloudiness { get; set; }
        public string H { get; set; }
        
        public string VV { get; set; }
        public string WeatherPhenomena { get; set; }
    }
}
