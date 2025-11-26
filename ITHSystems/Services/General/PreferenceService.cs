using ITHSystems.Attributes;

namespace ITHSystems.Services.General;
[RegisterService]
public class PreferenceService : IPreferenceService
{
    public string Get(string key, string defaultValue = "")
    {
        return Preferences.Get(key, defaultValue);
    }

    public void Set(string key, string value)
    {
        Preferences.Set(key, value);
    }
    public bool Exist(string key)
    {
        return !string.IsNullOrEmpty(Preferences.Get(key, string.Empty));
    }

}
