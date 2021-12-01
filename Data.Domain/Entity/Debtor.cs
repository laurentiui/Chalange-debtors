using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.Entity
{
    public class Debtor : BaseEntity
    {
        //[Key]
        //public int Number { get; set; }
        [Required]
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsClosed { get; set; }

        public bool Compare(Debtor anotherEntity) {
            //we do not consider here the IsClosed - because it has nothing to do with the debtors info regaring this particular sync
            return
                this.Id == anotherEntity.Id
                && this.Name == anotherEntity.Name
                && this.Telephone == anotherEntity.Telephone
                && this.Mobile == anotherEntity.Mobile
                && this.Email == anotherEntity.Email;
        }
    }

}
