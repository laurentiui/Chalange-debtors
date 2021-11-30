using Data.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByConfirmToken(string confirmToken);
        Task<User> GetByEmail(string email);
    }
}
