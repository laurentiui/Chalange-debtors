using Data.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBaseService<T>
        where T : BaseEntity {

        Task<IList<T>> ListAll();
        Task<T> GetById(int entityId);
        Task<T> Insert(T newEntity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}

