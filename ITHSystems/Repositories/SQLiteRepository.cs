
using SQLite;

namespace ITHSystems.Repositories;

public class SQLiteRepository<T> : IRepository<T> where T : class, new()
{
    public readonly ISQLiteAsyncConnection connection;
    public SQLiteRepository(ISQLiteManager maangeSQLite)
    {
         this.connection = maangeSQLite.Connection;
    }
    public async Task InsertAsync(T entity)
    {
       await connection.InsertAsync(entity);
    }

    public async Task DeleteAsync<TEntity>(object id) where TEntity : class, new()
    {
        var entity = await GetAsync<TEntity>(id);
        await connection.DeleteAsync(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()  where TEntity : class,new()
    {
       return await connection.Table<TEntity>().ToListAsync();
    }

    public Task<TEntity> GetAsync(this T object id) where TEntity : class, new()
    {
        return connection.FindAsync<TEntity>(id);
    }
   

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }
}
