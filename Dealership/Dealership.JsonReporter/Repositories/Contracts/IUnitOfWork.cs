﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.JsonReporter.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
