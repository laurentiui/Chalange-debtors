using Data.Domain.Entity;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        private readonly AppDbContext _appDbContext;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IList<T>> ListAll()
        {
            var list = _appDbContext.Set<T>().ToList();
            return list;
        }
        public async Task<T> GetById(int entityId)
        {
            var entity = _appDbContext.Set<T>().FirstOrDefault(ent => ent.Id == entityId);
            return entity;
        }

        public async Task<T> Insert(T newEntity)
        {
            _appDbContext.Set<T>().Add(newEntity);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);

            _appDbContext.Entry(newEntity).State = EntityState.Detached;
            return newEntity;
        }
        public async Task<T> Update(T entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }
        public async Task Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }

}
