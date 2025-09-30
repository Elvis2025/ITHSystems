using ITHSystems.Model;
using SQLite;
using System.Linq.Expressions;

namespace ITHSystems.Repositories.SQLite;

public interface IRepository<TEntity> where TEntity : IBaseEntity, new()
{
    Task DeleteAllAsync();
    Task DeleteAsync(TEntity entity);
    Task DeleteByIdAsync(object id);
    Task<int> ExecuteQueryAsync(string query);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereCondition);
    Task<TEntity> GetAsync(object id);
    Task InsertAllAsync(IEnumerable<TEntity> entity, bool runInTransaction = true);
    Task InsertAsync(TEntity entity);
    Task InsertOrReplaceAllAsync(IEnumerable<TEntity> entity, bool runInTransaction = true);
    Task InsertOrReplaceAsync(TEntity entity);
    Task<IEnumerable<TEntity>> QueryAsync(string query, params object[] args);
    Task UpdateAllAsync(IEnumerable<TEntity> entity, bool runInTransaction = true);
    Task UpdateAsync(TEntity entity);
}



