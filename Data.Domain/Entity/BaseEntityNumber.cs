using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.Entity
{
    public class BaseEntityNumber
    {
        [Key]
        public int Number { get; set; }
    }
}
