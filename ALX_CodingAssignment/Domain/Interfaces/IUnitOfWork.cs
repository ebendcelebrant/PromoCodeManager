using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALX_CodingAssignment.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
    }
}
