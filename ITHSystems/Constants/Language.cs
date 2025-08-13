using ITHSystems.Resx;

namespace ITHSystems.Constants;
public sealed class Language
{
    public static Language Spanish = new(IBSResources.Spanish, "es-ES");
    public static Language English = new(IBSResources.English, "en-US");
    public static Language French = new(IBSResources.French, "fr-FR");
    public static Language Portuguese = new(IBSResources.Portuguese, "pt-PT");
    public static Language Italian = new(IBSResources.Italian, "it-IT");
    public static Language German = new(IBSResources.German, "de-DE");

    public string Name { get; }
    public string Code { get; }

    private Language(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public override string ToString() => Code;

    public static IEnumerable<Language> List() => new[] { English, Spanish, French, Portuguese, Italian, German };
    public static Language FindLanguage(string code) => List().FirstOrDefault(x => x.Code == code) ?? Spanish;

    public static Language FromCode(string code) =>
        List().FirstOrDefault(x => x.Code == code) ?? English;

    public static Language FromName(string name) =>
        List().FirstOrDefault(x => x.Name == name) ?? English;
}