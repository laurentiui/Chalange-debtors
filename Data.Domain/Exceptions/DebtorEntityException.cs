using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.Exceptions
{
    public class DebtorEntityException : Exception
    {
        public DebtorEntityException(string message) : base(message) {

        }
    }
}
