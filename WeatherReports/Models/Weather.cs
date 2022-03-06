using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReports.Models
{
    public class Weather
    {
        public DateOnly Data { get; set; }
        public TimeOnly Time { get; set; }
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
