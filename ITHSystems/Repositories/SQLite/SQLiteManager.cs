using ITHSystems.Attributes;
using ITHSystems.Constants;
using SQLite;
using System.Reflection;

namespace ITHSystems.Repositories.SQLite;
[RegisterRepository]
public class SQLiteManager : ISQLiteManager
{
    public ISQLiteAsyncConnection Connection => new SQLiteAsyncConnection(SQLiteConfiguration.DBPath, SQLiteOpenFlags.ReadWrite | 
                                                                          SQLiteOpenFlags.Create | 
                                                                          SQLiteOpenFlags.SharedCache, true);

    public async Task CreateTables()
    {

        var allTypes = AppDomain.CurrentDomain.GetAssemblies()
                                              .SelectMany(a =>
                                              {
                                                  try { return a.GetTypes(); }
                                                  catch { return Array.Empty<Type>(); }
                                              });

        var modelTypes = allTypes.Where(t => t.IsClass &&
                                             t.Namespace is not null &&
                                             t.Namespace == "Model" &&
                                             t.GetCustomAttribute<SQLiteEntityAttribute>() is not null);

        foreach (var type in modelTypes)
        {
            var method = typeof(SQLiteAsyncConnection).GetMethods()
                                                      .First(m => m.Name == nameof(SQLiteAsyncConnection.CreateTableAsync) 
                                                                         && m.IsGenericMethod);

            var genericMethod = method.MakeGenericMethod(type);

            await (Task)genericMethod.Invoke(Connection, new object[] { null! })!;
        }
    }
    public void CreateTablesUnAsync()
    {

        var allTypes = AppDomain.CurrentDomain.GetAssemblies()
                                              .SelectMany(a =>
                                              {
                                                  try { return a.GetTypes(); }
                                                  catch { return Array.Empty<Type>(); }
                                              });

        var modelTypes = allTypes.Where(t => t.IsClass &&
                                             t.Namespace is not null &&
                                             t.Namespace.Contains("Model") &&
                                             t.GetCustomAttribute<SQLiteEntityAttribute>() is not null);

        foreach (var type in modelTypes)
        {
            var method = typeof(SQLiteAsyncConnection).GetMethods()
                                                      .First(m => m.Name == nameof(SQLiteAsyncConnection.CreateTableAsync)
                                                                         && m.IsGenericMethod);

            var genericMethod = method.MakeGenericMethod(type);
            _ = Task.Run(async () =>
            {
                await (Task)genericMethod.Invoke(Connection, new object[] { null! })!;
            });
        }
    }

}
