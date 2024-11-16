using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace QuizForAndroid.DAL.GenericBase
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetTableAsTracking();
        IQueryable<T> GetTableNoTracking();
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
    }
}

