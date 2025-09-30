using ITHSystems.Model;
using SQLite;
using System.Linq.Expressions;

namespace ITHSystems.Repositories.SQLite;

public class SQLiteRepository<TEntity> : IRepository<TEntity> where TEntity : IBaseEntity,new()
{
    public readonly ISQLiteAsyncConnection connection;
    private readonly IRepository<TEntity> entityRepository;

    public SQLiteRepository(ISQLiteManager maangeSQLite,IRepository<TEntity> entityRepository)
    {
         connection = maangeSQLite.Connection;
        this.entityRepository = entityRepository;
    }


    #region Insert Methods
    public async Task InsertAsync(TEntity entity)
    {
       await connection.InsertAsync(entity);
    }
    public async Task InsertAllAsync(IEnumerable<TEntity> entity, bool runInTransaction = true)
    {
       await connection.InsertAllAsync(entity, runInTransaction);
    }

    public async Task InsertOrReplaceAsync(TEntity entity)
    {
       await connection.InsertOrReplaceAsync(entity);
    }

    public async Task InsertOrReplaceAllAsync(IEnumerable<TEntity> entity, bool runInTransaction = true)
    {
        if (!runInTransaction)
        {
            await connection.InsertOrReplaceAsync(entity);
            return;
        }
        await connection.RunInTransactionAsync(async (conn) =>
        {
            foreach (var item in entity)
            {
                await connection.InsertOrReplaceAsync(entity);
            }
        });

    }
    #endregion

    #region Get Methods
    public async Task<IEnumerable<TEntity>> GetAllAsync() 
    {
       return await connection.Table<TEntity>().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> whereCondition) 
    {
       return await connection.Table<TEntity>()
                              .Where(whereCondition)
                              .ToListAsync();
    }

    public Task<TEntity> GetAsync(object id)
    {
        return connection.GetAsync<TEntity>(id);
    }
    #endregion

    #region Update Methods
    public async Task UpdateAsync(TEntity entity)
    {
        await connection.UpdateAsync(entity);
    }
    public async Task UpdateAllAsync(IEnumerable<TEntity> entity,bool runInTransaction = true)
    {
        await connection.UpdateAllAsync(entity, runInTransaction);
    }
    #endregion

    #region Delete Methods
    public async Task DeleteByIdAsync(object id)
    {
        var entitys = await entityRepository.GetAsync(id);
        await connection.DeleteAsync(entitys);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await connection.DeleteAsync(entity);
    }

    public async Task DeleteAllAsync()
    {
        await connection.DeleteAllAsync<TEntity>();
    }
    #endregion

    #region Query Methods
    public async Task<int> ExecuteQueryAsync(string query)
    {
        return await connection.ExecuteAsync(query);
    }

    public async Task<IEnumerable<TEntity>> QueryAsync(string query,params object[] args)
    {
       return await connection.QueryAsync<TEntity>(query, args);
    }

    #endregion


}
