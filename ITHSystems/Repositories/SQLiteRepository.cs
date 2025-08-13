
using SQLite;

namespace ITHSystems.Repositories;

public class SQLiteRepository<T> : IRepository<T> where T : class, new()
{
    private readonly ISQLiteAsyncConnection connection;
    public SQLiteRepository(ISQLiteManager maangeSQLite)
    {
         this.connection = maangeSQLite.Connection ?? throw new ArgumentNullException(nameof(maangeSQLite.Connection), "Connection cannot be null.");
    }
    public Task AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync(object id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
