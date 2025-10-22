using ITHSystems.Model;
using ITHSystems.Views;
using SQLite;
using System.Linq.Expressions;

namespace ITHSystems.Repositories.SQLite;

public class SQLiteRepository<TEntity> : IRepository<TEntity> where TEntity : IBaseEntity, new()
{
    public readonly ISQLiteAsyncConnection connection;

    public SQLiteRepository(ISQLiteManager maangeSQLite)
    {
        connection = maangeSQLite.Connection;
    }


    #region Insert Methods
    public async Task InsertAsync(TEntity entity)
    {
        try
        {
            await connection.InsertAsync(entity);
        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
        }
    }
    public async Task InsertAllAsync(IEnumerable<TEntity> entity, bool runInTransaction = true)
    {
        try
        {
            await connection.InsertAllAsync(entity, runInTransaction);
        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
        }
    }

    public async Task InsertOrReplaceAsync(TEntity entity)
    {
        try
        {
            await connection.InsertOrReplaceAsync(entity);
        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
        }

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
        try
        {
            return await connection.Table<TEntity>().ToListAsync();
        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
            return new List<TEntity>();
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereCondition)
    {
        try
        {
            return await connection.Table<TEntity>()
                                   .Where(whereCondition)
                                   .ToListAsync();
        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
            return new List<TEntity>();
        }
    }

    public Task<TEntity> GetAsync(object id)
    {
        return connection.GetAsync<TEntity>(id);
    }
    #endregion

    #region Update Methods
    public async Task UpdateAsync(TEntity entity)
    {
        try
        {
            await connection.UpdateAsync(entity);
        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
        }
    }
    public async Task UpdateAllAsync(IEnumerable<TEntity> entity, bool runInTransaction = true)
    {
        try
        {
            await connection.UpdateAllAsync(entity, runInTransaction);

        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
        }
    }
    #endregion

    #region Delete Methods
    public async Task DeleteByIdAsync(object id)
    {
        try
        {
            var entity = await connection.FindAsync<TEntity>(id);
            if (entity is null)
            {
                await BaseViewModel.ErrorAlert("Error", "Entity not found");
                return;
            }
            await connection.DeleteAsync(entity);

        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
        }
    }

    public async Task DeleteAsync(TEntity entity)
    {
        try
        {
            await connection.DeleteAsync(entity);

        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
        }
    }

    public async Task DeleteAllAsync()
    {
        try
        {
            await connection.DeleteAllAsync<TEntity>();
        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
        }
    }
    #endregion

    #region Query Methods
    public async Task<int> ExecuteQueryAsync(string query)
    {
        try
        {
            return await connection.ExecuteAsync(query);
        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
            return -1;
        }
    }

    public async Task<IEnumerable<TEntity>> QueryAsync(string query, params object[] args)
    {
        try
        {
            return await connection.QueryAsync<TEntity>(query, args);
        }
        catch (Exception e)
        {
            await BaseViewModel.ErrorAlert("Error", e.Message);
            return new List<TEntity>();
        }
    }

    #endregion


}
