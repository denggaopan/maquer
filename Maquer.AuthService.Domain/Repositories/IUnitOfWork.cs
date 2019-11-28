using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.AuthService.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        bool SaveChanges();
    }
}
