namespace ITHSystems.Repositories;

public interface IRepository<T> where T : class,new()
{
    Task<T> GetAsync(object id);
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(object id);
    Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class, new();
}


