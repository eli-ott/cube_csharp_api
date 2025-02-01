using System.Linq.Expressions;

namespace MonApi.Shared.Services
{
    public interface IBaseService<TModel> where TModel : class
    {
        Task<TModel> AddAsync(TModel model, CancellationToken cancellationToken = default);
        Task UpdateAsync(TModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(TModel model, CancellationToken cancellationToken = default);
        Task<TModel?> FindAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
        Task<List<TModel>> ListAsync(CancellationToken cancellationToken = default);
        Task<List<TModel>> ListAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken);
        Task<TModel?> FirstOrDefaultAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
