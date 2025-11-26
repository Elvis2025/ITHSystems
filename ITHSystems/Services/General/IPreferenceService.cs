namespace ITHSystems.Services.General;

public interface IPreferenceService
{
    bool Exist(string key);
    string Get(string key, string defaultValue = "");
    void Set(string key, string value);
}
