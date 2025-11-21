using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.Repositories.Concrete;
using RepositoryLayer.UnitOfWorks.Abstract;

namespace RepositoryLayer.UnitOfWorks.Concrete
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        #region Why we use SaveChangesAsync here ?
        // We use SaveChangesAsync to avoid blocking the main thread while the database 
        // operation is in progress. This allows the server to handle other incoming 
        // requests efficiently, improving performance and scalability, especially 
        // under high load.
        #endregion
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        #region Why We use ValueTask here ?
        // DisposeAsync is used to release the DbContext and database connections 
        // asynchronously. Using ValueTask instead of Task makes the operation lighter 
        // since disposing is usually a quick operation that doesn't always require 
        // a full async Task. This prevents blocking the thread and avoids resource leaks.
        #endregion
        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        IGenericRepositories<T> IUnitOfWork.GetGenericRepository<T>()
        {
            // Get Obj from GenericRepository
            return new GenericRepositories<T>(_context);

        }
    }
}
