using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WeatherReports.ViewModels
{
    public class WeatherViewModel
    {[Display(Name ="Дата")]
        public DateOnly Data { get; set; }
        [Display(Name = "Время")]
        public TimeOnly Time { get; set; }
        [Display(Name = "Температура")]
        public double Temperature { get; set; }
        [Display(Name = "Относительная влажность")]
        public int Humidity { get; set; }
        [Display(Name = "ТД")]
        public double Td { get; set; }
        [Display(Name = "Атмосферное давление")]
        public int AtmosphericPressure { get; set; }
        [Display(Name = "Направление ветра")]
        public string WindDirection { get; set; }
        [Display(Name = "Скорость ветра")]
        public int WindSpeed { get; set; }
        [Display(Name = "Облачность")]
        public int Cloudiness { get; set; }
        [Display(Name = "H")]
        public int H { get; set; }
        [Display(Name = "VV")]
        public int VV { get; set; }
        [Display(Name = "Погодные явления")]
        public string WeatherPhenomena { get; set; }
    }
}
