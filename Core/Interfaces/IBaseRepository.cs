using Core.Entities;
using Core.Specifications;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
     
        Task AddAsync(T entity);
       
        Task<T> UpdateAsync(int id, T entity);
        Task<T> DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(int id, ISpecification<T> spec);

    }
}
