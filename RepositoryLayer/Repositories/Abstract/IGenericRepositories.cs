using CoreLayer.BaseEntities;
using System.Linq.Expressions;

namespace RepositoryLayer.Repositories.Abstract
{
    // <T> here is class or any class Inherits from IBaseEntity.
    public interface IGenericRepositories<T> where T : class, IBaseEntity, new()
    {
        Task AddEntityAsync(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
        IQueryable<T> GetAllEntityList();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<T> GetEntityByIdAsync(int id);
    }
}
