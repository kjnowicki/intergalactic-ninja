using System.Collections.Generic;

public enum Language
{
    English = 0,
    Polish = 1
}

internal static class LanguageExtensions
{
    private static Dictionary<Language, string> _languageNameDictionary = new Dictionary<Language, string>()
    {
        { Language.English, "eng" },
        { Language.Polish, "pl" }
    };

    public static string GetStringValue(this Language language)
    {
        return _languageNameDictionary[language];
    }
}
