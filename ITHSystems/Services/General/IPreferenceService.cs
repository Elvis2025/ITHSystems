namespace ITHSystems.Services.General;

public interface IPreferenceService
{
    string Get(string key, string defaultValue = "");
    void Set(string key, string value);
}
