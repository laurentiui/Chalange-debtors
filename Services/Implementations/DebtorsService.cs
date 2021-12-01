using Data.Domain.Entity;
using Data.Domain.Exceptions;
using Data.Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class DebtorsService : BaseService<Debtor>, IDebtorsService
    {
        private readonly IDebtorsRepository _debtorsRepository;

        public DebtorsService(IDebtorsRepository debtorsRepository) : base(debtorsRepository) {
            _debtorsRepository = debtorsRepository;
        }

        public new async Task<Debtor> Update(Debtor entity) {
            //we check for null because we don't want to hide updating a non-existing item error
            var existingItem = await _debtorsRepository.GetById(entity.Id);
            if (existingItem != null) {
                if (existingItem.IsClosed) {
                    throw new DebtorEntityException("can not update a closed entity");
                }
            }
            entity = await _debtorsRepository.Update(entity);
            return entity;
        }
    }
}
