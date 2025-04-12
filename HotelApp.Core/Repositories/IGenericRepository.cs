using System.Linq.Expressions;

namespace HotelApp.Core.Repositories
{
    /// <summary>
    /// Общо репозитори за основните CRUD операции за работа с базата данни
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Delete(int id);
        Task DeleteAsync(int id);
        void Delete(T entity);
        Task DeleteAsync(T entity);
        void Save();
        Task SaveAsync();
        Task<List<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes);
        Task<bool> ExistsAsync(int id);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

    }
}
