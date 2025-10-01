using SQLite;

namespace ITHSystems.Repositories.SQLite;

public interface ISQLiteManager
{
    ISQLiteAsyncConnection Connection { get; }

    Task CreateTables();
    void CreateTablesUnAsync();
}
