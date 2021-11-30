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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> GetByConfirmToken(string confirmToken)
        {
            var user = await _appDbContext.Set<User>().FirstOrDefaultAsync(u => u.ConfirmToken == confirmToken);
            return user;
        }
        public async Task<User> GetByEmail(string email)
        {
            var user = await _appDbContext.Set<User>().FirstOrDefaultAsync(u => u.Email.ToLower() == email);
            return user;
        }
    }
}
