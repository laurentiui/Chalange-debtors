using Data.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<User> LoginAsync(string email, string password);
        Task<User> RegisterAsync(string username, string email, string password);
        Task<User> ConfirmUserAsync(string confirmToken);
    }
}
