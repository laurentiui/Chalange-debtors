﻿using Data.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDebtorsService
    {
        Task<IList<Debtor>> ListAll();
    }
}