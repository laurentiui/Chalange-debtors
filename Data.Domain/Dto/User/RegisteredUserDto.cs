using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.Dto.User
{
    public class RegisteredUserDto
    {
        public string Username { get; set; }
        public string ConfirmToken { get; set; }
    }
}
