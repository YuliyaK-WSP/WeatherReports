using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherReports.Models;

namespace WeatherReports.ViewModels
{
    public class WeatherViewModel
    {[Display(Name ="Дата")]
        public string Data { get; set; }
        [Display(Name = "Время")]
        public string Time { get; set; }
        [Display(Name = "Температура")]
        public string Temperature { get; set; }
        [Display(Name = "Относительная влажность")]
        public string Humidity { get; set; }
        [Display(Name = "ТД")]
        public string Td { get; set; }
        [Display(Name = "Атмосферное давление")]
        public string AtmosphericPressure { get; set; }
        [Display(Name = "Направление ветра")]
        public string WindDirection { get; set; }
        [Display(Name = "Скорость ветра")]
        public string WindSpeed { get; set; }
        [Display(Name = "Облачность")]
        public string Cloudiness { get; set; }
        [Display(Name = "H")]
        public string H { get; set; }
        [Display(Name = "VV")]
        public string VV { get; set; }
        [Display(Name = "Погодные явления")]
        public string WeatherPhenomena { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
