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

        public async Task<Debtor> Close(Debtor entity) {
            entity.IsClosed = true;

            entity = await _debtorsRepository.Update(entity);
            return entity;
        }

        public async Task<DebtorSync> GetListsForUpdateData(IList<Debtor> debtorsToImport) {
            var debtorsExisting = await _debtorsRepository.ListAll();

            var listDebtorsToImportIds = debtorsToImport.Select(d => d.Id).ToList();
            var listDebtorsExistingIds = debtorsExisting.Select(d => d.Id).ToList();

            // invalid ids - exsting in file, but debtors are already closed
            // toImport INTERSECT exsting - but existing are closed
            var debtorsInvalid = debtorsExisting.Where(d =>
                listDebtorsExistingIds.Intersect(listDebtorsToImportIds).Contains(d.Id)
                && d.IsClosed == true
            ).ToList();

            // add = toImport MINUS existing
            var debtorsToAdd = debtorsToImport.Where(d =>
                listDebtorsToImportIds.Except(listDebtorsExistingIds).Contains(d.Id)
            ).ToList();

            // close = existing MINUS toImport - but in existing they are not closed
            var debtorsToClose = debtorsExisting.Where(d =>
                listDebtorsExistingIds.Except(listDebtorsToImportIds).Contains(d.Id)
                && !d.IsClosed
            ).ToList();

            // potential update = toIMPORT INTERSECT existing, not closed
            var debtorsToPotentionalUpdateIds = debtorsExisting.Where(d =>
                listDebtorsToImportIds.Intersect(listDebtorsExistingIds).Contains(d.Id)
                && d.IsClosed == false
            ).Select(d => d.Id).ToList();
            var debtorsToPotentionalUpdate = debtorsToImport.Where(d => debtorsToPotentionalUpdateIds.Contains(d.Id)).ToList();

            // toIgnore - both in db and import, but same props
            var debtorsToIgnore = debtorsToPotentionalUpdate.Where(d => {
                var debtorExisting = debtorsExisting.First(dimp => dimp.Id == d.Id);
                return debtorExisting.Compare(d);
            }).ToList();

            // to update - both in db and import, different props
            var debtorsToUpdate = debtorsToPotentionalUpdate.Where(d => {
                var debtorExisting = debtorsExisting.First(dimp => dimp.Id == d.Id);
                return !debtorExisting.Compare(d);
            }).ToList();

            return new DebtorSync() {
                ListInvalid = debtorsInvalid,
                ListToIgnore = debtorsToIgnore,
                ListToAdd = debtorsToAdd,
                ListToClose = debtorsToClose,
                ListToUpdate = debtorsToUpdate
            };
        }

        public async Task<IList<Debtor>> UpdateData(IList<Debtor> debtorsToImport) {
            var debtorSync = await this.GetListsForUpdateData(debtorsToImport);

            //insert
            foreach(var debtor in debtorSync.ListToAdd) {
                await this.Insert(debtor);
            }

            //update
            foreach (var debtor in debtorSync.ListToUpdate) {
                await this.Update(debtor);
            }

            //close
            foreach (var debtor in debtorSync.ListToClose) {
                await this.Close(debtor);
            }

            var newList = await this.ListAll();

            return newList;
        }
    }
}
