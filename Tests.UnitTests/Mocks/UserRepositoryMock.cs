using Data.Domain.Entity;
using Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.UnitTests.Mocks
{
    public class UserRepositoryMock : BaseRepositoryMock<User>, IUserRepository
    {
        public async Task<User> GetByConfirmToken(string confirmToken)
        {
            return await Task.Run(() => _list.FirstOrDefault(entity => entity.ConfirmToken == confirmToken));
        }
        public async Task<User> GetByEmail(string email)
        {
            return await Task.Run(() => _list.FirstOrDefault(entity => entity.Email == email));
        }
    }
}
