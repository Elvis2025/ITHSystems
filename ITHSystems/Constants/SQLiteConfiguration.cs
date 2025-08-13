namespace ITHSystems.Constants;

public static class SQLiteConfiguration
{
    private const string DatabaseName = "IBSystems.db3";
    public static string DBPath => Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
}
