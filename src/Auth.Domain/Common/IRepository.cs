using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Domain.Common
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}