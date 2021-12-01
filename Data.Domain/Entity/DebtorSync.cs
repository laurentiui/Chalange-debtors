using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.Entity
{
    public class DebtorSync
    {
        public IList<Debtor> ListInvalid { get; set; }
        public IList<Debtor> ListToIgnore { get; set; }

        public IList<Debtor> ListToAdd { get; set; }
        public IList<Debtor> ListToClose { get; set; }
        public IList<Debtor> ListToUpdate { get; set; }
    }
}
