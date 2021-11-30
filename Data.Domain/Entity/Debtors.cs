using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.Entity
{
    public class Debtors : BaseEntity
    {
        public int Number { get; set; }
        public int Name { get; set; }
    }
}
