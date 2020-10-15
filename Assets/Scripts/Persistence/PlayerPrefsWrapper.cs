using UnityEngine;
public class PlayerPrefsWrapper
{
    private const string _LANGUAGE_KEY = "lang";
    private const string _KILLER_BEST_SCORE = "killerBestScore";
    private const string _HACKING_CONTROL_METHOD = "hackingControlMethod";

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

    public static int GetKillerBestScore()
    {
        int defaultValue = 0;
        if (!PlayerPrefs.HasKey(_KILLER_BEST_SCORE))
        {
            SetKillerBestScoreAndSave(defaultValue);
        }
        return PlayerPrefs.GetInt(_KILLER_BEST_SCORE);
    }

    public static void SetKillerBestScoreAndSave(int value)
    {
        PlayerPrefs.SetInt(_KILLER_BEST_SCORE, value);
        PlayerPrefs.Save();
    }

    public static ControlMethod GetHackingControlMethod()
    {
        ControlMethod defaultValue = ControlMethod.Swipe;
        if (!PlayerPrefs.HasKey(_HACKING_CONTROL_METHOD))
        {
            SetHackingControlMethod(defaultValue);
        }

        return (ControlMethod)PlayerPrefs.GetInt(_HACKING_CONTROL_METHOD);
    }

    public static void SetHackingControlMethod(ControlMethod value)
    {
        PlayerPrefs.SetInt(_HACKING_CONTROL_METHOD, (int)value);
        PlayerPrefs.Save();
    }
}
