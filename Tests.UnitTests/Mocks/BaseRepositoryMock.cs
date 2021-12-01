using Data.Domain.Entity;
using Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UnitTests.Mocks
{
    public class BaseRepositoryMock<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        public List<T> _list;

        public BaseRepositoryMock()
        {
            _list = new List<T>();
        }

        public async Task<IList<T>> ListAll()
        {
            return await Task.Run(() => _list);
        }
        public async Task<T> GetById(int entityId)
        {
            return await Task.Run(() => _list.FirstOrDefault(entity => entity.Id == entityId));
        }

        public async Task<T> Insert(T newEntity)
        {
            return await Task.Run(() =>
            {
                //to mock the "key" error
                if (_list.Any(item => item.Id == newEntity.Id)) {
                    throw new Exception("Cannot insert explicit value for identity column");
                }
                _list.Add(newEntity);
                return newEntity;
            });
        }
        public async Task<T> Update(T entity)
        {
            return await Task.Run(() =>
            {
                var toUpdate = _list.FirstOrDefault(l => l.Id == entity.Id);
                _list.Remove(toUpdate);
                _list.Add(entity);

                return entity;
            });
        }
        public async Task Delete(T entity)
        {
            await Task.Run(() =>
            {
                _list.Remove(entity);
            });
        }

        public async Task ClearAll() {
            _list.Clear();
        }
    }
}
