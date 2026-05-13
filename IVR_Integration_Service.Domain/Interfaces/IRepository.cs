using System.Linq.Expressions;

namespace IVR_Integration_Service.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T>              GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task<T>              AddAsync(T entity);
        Task                 UpdateAsync(T entity);
    }
}
