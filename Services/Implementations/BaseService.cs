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
    public class BaseService<T> : IBaseService<T>
        where T : BaseEntity {

        private readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository) {
            _repository = repository;
        }
        public async Task<IList<T>> ListAll() {
            var list = await _repository.ListAll();
            return list;
        }
        public async Task<T> GetById(int entityId) {
            var entity = await _repository.GetById(entityId);
            return entity;
        }

        public async Task<T> Insert(T newEntity) {
            var newEntiry = await _repository.Insert(newEntity);
            return newEntity;
        }
        public async Task<T> Update(T entity) {
            entity = await _repository.Update(entity);
            return entity;
        }
        public async Task Delete(T entity) {
            await _repository.Delete(entity);
        }
    }
}
