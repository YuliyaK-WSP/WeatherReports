using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherReports.Domain.Entities.Base.Interfase;

namespace WeatherReports.Domain.Entities.Base
{
    public abstract class Entity:IEntity
    {
        public int Id { get; set; }
    }
}
