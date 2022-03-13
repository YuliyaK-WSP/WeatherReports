using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherReports.Domain.Entities.Base;

namespace WeatherReports.Models
{
    public class ArchiveViewModel
    {
        public IEnumerable<Weather> Weathers { get; set; }
        public PageViewModel PageViewModel { get; set; }//Указанная страниа
        public string SelectedMonth { get; set; }// Введенный месяц
        public string SelectedYear { get; set; }//Введенный год

    }
}
