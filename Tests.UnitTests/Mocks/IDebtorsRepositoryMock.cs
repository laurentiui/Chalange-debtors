using Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UnitTests.Mocks
{
    public interface IDebtorsRepositoryMock : IDebtorsRepository {
        Task ClearAll();
    }
}
