using Data.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDebtorsService : IBaseService<Debtor>
    {
        Task<Debtor> Close(Debtor entity);

        Task<DebtorSync> GetListsForUpdateData(IList<Debtor> debtorsToImport);
        Task<IList<Debtor>> UpdateData(IList<Debtor> debtorsToImport);
    }
}
