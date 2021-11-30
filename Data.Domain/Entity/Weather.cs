using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.Entity
{
    public class Weather : BaseEntity
    {
        public DateTime Day { get; set; }
        public int TemperatureCelsius { get; set; }
        public string Summary => TemperatureCelsius <= 10 ? "cold" : "warm";
    }
}
