using Data.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IDebtorsRepository : IBaseRepository<Debtor> {
        //Task<IList<Debtor>> ListAll();
    }
}
