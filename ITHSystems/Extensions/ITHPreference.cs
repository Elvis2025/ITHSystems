using ITHSystems.Services.General;

namespace ITHSystems.Extensions;

public sealed class ITHPreference
{
    private static readonly Lazy<PreferenceService> _instance =
        new Lazy<PreferenceService>(() => new PreferenceService());

    public static PreferenceService Instance => _instance.Value;

    // Constructor privado
    private ITHPreference() { }

    public static string Get(string key, string defaultValue = "")
    {
        return Instance.Get(key, defaultValue);
    }

    public static void Set(string key, string value)
    {
        Instance.Set(key, value);
    }
}
