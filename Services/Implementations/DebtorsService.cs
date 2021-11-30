using Data.Domain.Entity;
using Data.Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class DebtorsService : IDebtorsService
    {
        private readonly IDebtorsRepository _debtorsRepository;

        public DebtorsService(IDebtorsRepository debtorsRepository) {
            _debtorsRepository = debtorsRepository;
        }
        public async Task<IList<Debtor>> ListAll() {
            var list = await _debtorsRepository.ListAll();
            return list;
        }
    }
}
