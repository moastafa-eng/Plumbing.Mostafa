using CoreLayer.BaseEntities;
using RepositoryLayer.Repositories.Abstract;

namespace RepositoryLayer.UnitOfWorks.Abstract
{
    public interface IUnitOfWork
    {
        void Commit();

        #region Why we use SaveChangesAsync here ?
        // We use SaveChangesAsync to avoid blocking the main thread while the database 
        // operation is in progress. This allows the server to handle other incoming 
        // requests efficiently, improving performance and scalability, especially 
        // under high load.
        #endregion
        Task CommitAsync(); // Work with out blocking thread
        IGenericRepositories<T> GetGenericRepository<T>() where T : class, IBaseEntity, new();

        #region Why We use ValueTask here ?
        // DisposeAsync is used to release the DbContext and database connections 
        // asynchronously. Using ValueTask instead of Task makes the operation lighter 
        // since disposing is usually a quick operation that doesn't always require 
        // a full async Task. This prevents blocking the thread and avoids resource leaks.
        #endregion
        ValueTask DisposeAsync();
    }
}
