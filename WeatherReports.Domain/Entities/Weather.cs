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
        public DateTime Data { get; set; }
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        
        public int Humidity { get; set; }
        public double Td { get; set; }
        public int AtmosphericPressure { get; set; }
        public string WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public int Cloudiness { get; set; }
        public int H { get; set; }
        public int VV { get; set; }
        public string WeatherPhenomena { get; set; }
    }
}
