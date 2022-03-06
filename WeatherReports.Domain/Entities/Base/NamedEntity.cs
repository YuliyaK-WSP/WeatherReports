using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherReports.Domain.Entities.Base.Interfase;

namespace WeatherReports.Domain.Entities.Base
{
    public class NamedEntity:Entity,INamedEntity
    {
        public string Name { get; set; }
    }
}
