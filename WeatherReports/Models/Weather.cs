using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReports.Models
{
    public class Weather
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
