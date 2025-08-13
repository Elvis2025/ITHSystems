using ITHSystems.Attributes;
using ITHSystems.Constants;
using ITHSystems.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ITHSystems.Repositories
{
    public class SQLiteManager : ISQLiteManager
    {
        public ISQLiteAsyncConnection Connection => new SQLiteAsyncConnection(SQLiteConfiguration.DBPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache, true);

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
                                                 t.Namespace.Contains("Model") &&
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
    }
}
