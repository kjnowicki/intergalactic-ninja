using UnityEngine;
public class PlayerPrefsWrapper
{
    private const string _LANGUAGE_KEY = "lang";

    public static Language GetLanguage(Language defaultLanguage = Language.English)
    {
        if (!PlayerPrefs.HasKey(_LANGUAGE_KEY))
        {
            return defaultLanguage;
        }
        int languageValue = PlayerPrefs.GetInt(_LANGUAGE_KEY);
        return (Language)languageValue;
    }

    public static void SetLanguageAndSave(Language language)
    {
        PlayerPrefs.SetInt(_LANGUAGE_KEY, (int)language);
        PlayerPrefs.Save();
    }
}
