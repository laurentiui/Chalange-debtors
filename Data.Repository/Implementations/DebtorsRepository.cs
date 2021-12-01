using Data.Domain.Entity;
using Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Implementations {
    public class DebtorsRepository : BaseRepository<Debtor>, IDebtorsRepository {
        private readonly AppDbContext _appDbContext;

        public DebtorsRepository(AppDbContext appDbContext) : base(appDbContext) {
            _appDbContext = appDbContext;
        }

    }
}
