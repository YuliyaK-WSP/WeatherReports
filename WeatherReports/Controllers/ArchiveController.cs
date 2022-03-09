using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReports.Controllers
{
    public class ArchiveController:Controller
    {
        [HttpPost]
        public ActionResult Filter(string month, string year)
        {
            string s1 = month;//
            string s2 = year;//
            return RedirectToAction("Archive", "File", new {month = s1, year = s2});
        }

    }
}
