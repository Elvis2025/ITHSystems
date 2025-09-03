namespace ITHSystems.Repositories;

public interface IRepository<T> where T : class,new()
{
    Task<T> GetAsync(object id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(object id);
}


