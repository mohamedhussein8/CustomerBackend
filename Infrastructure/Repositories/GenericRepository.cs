using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; set; }
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context)
        {
            Table = context.Set<T> ();
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public virtual async Task<T> DeleteAsync(int id)
        {
            var entity = await Table.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
            {
                return null;
            }
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

      

        public virtual async  Task<T> GetByIdAsync(int id)
        {
            return await Table.FirstOrDefaultAsync( c => c.Id.Equals(id));
        }
        

        public virtual async Task<T> UpdateAsync(int id, T entity)
        {
            var local = Table.FirstOrDefault(entry => entry.Id.Equals(id));

            if (local != null)
            {
              
                _context.Entry(local).State = EntityState.Detached;
            }
            else
            {
                return null;

            }
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }



        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).AsNoTrackingWithIdentityResolution().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id, ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync(e => e.Id == id);
        }


        // Helper Method
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(Table.AsQueryable(), spec);
        }
    }
}
