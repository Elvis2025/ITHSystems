using SQLite;

namespace ITHSystems.Repositories;

public interface ISQLiteManager
{
    ISQLiteAsyncConnection Connection { get; }

    Task CreateTables();
    void CreateTablesUnAsync();
}
