using System;
using System.Collections.Generic;
using System.Text;

namespace Maquer.Domain.User.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        bool SaveChanges();
    }
}
