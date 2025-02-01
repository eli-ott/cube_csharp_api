using MonApi.Shared.Repositories;
using System.Linq.Expressions;

namespace MonApi.Shared.Services
{
    public class BaseService<TModel> : IBaseService<TModel> where TModel : class
    {
        protected readonly IBaseRepository<TModel> _repository;

        public BaseService(IBaseRepository<TModel> repository)
        {
            _repository = repository;
        }

        public virtual async Task<TModel> AddAsync(TModel model, CancellationToken cancellationToken = default)
        {
            return await _repository.AddAsync(model, cancellationToken);
        }

        public virtual async Task UpdateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            await _repository.UpdateAsync(model, cancellationToken);
        }

        public virtual async Task DeleteAsync(TModel model, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(model, cancellationToken);
        }

        public virtual async Task<TModel?> FindAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await _repository.FindAsync(id, cancellationToken);
        }

        public virtual async Task<List<TModel>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.ListAsync(cancellationToken);
        }

        public virtual async Task<List<TModel>> ListAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _repository.ListAsync(predicate, cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _repository.AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repository.CountAsync(predicate, cancellationToken);
        }

        public virtual async Task<TModel?> FirstOrDefaultAsync(Expression<Func<TModel, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _repository.FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
