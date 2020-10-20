using UnityEngine;
public class PlayerPrefsWrapper
{
    private const string LANGUAGE_KEY = "lang";
    private const string KILLER_BEST_SCORE = "killerBestScore";
    private const string HACKING_BEST_SCORE = "hackingBestScore";
    private const string HACKING_CONTROL_METHOD = "hackingControlMethod";

    public static Language GetLanguage(Language defaultLanguage = Language.English)
    {
        if (!PlayerPrefs.HasKey(LANGUAGE_KEY))
        {
            return defaultLanguage;
        }
        int languageValue = PlayerPrefs.GetInt(LANGUAGE_KEY);
        return (Language)languageValue;
    }

    public static void SetLanguageAndSave(Language language)
    {
        PlayerPrefs.SetInt(LANGUAGE_KEY, (int)language);
        PlayerPrefs.Save();
    }

    public static int GetKillerBestScore()
    {
        int defaultValue = 0;
        if (!PlayerPrefs.HasKey(KILLER_BEST_SCORE))
        {
            SetKillerBestScoreAndSave(defaultValue);
        }
        return PlayerPrefs.GetInt(KILLER_BEST_SCORE);
    }

    public static void SetKillerBestScoreAndSave(int value)
    {
        PlayerPrefs.SetInt(KILLER_BEST_SCORE, value);
        PlayerPrefs.Save();
    }

    public static int GetHackingBestScore()
    {
        int defaultValue = 0;
        if (!PlayerPrefs.HasKey(HACKING_BEST_SCORE))
        {
            SetHackingBestScoreAndSave(defaultValue);
        }
        return PlayerPrefs.GetInt(HACKING_BEST_SCORE);
    }

    public static void SetHackingBestScoreAndSave(float value)
    {
        PlayerPrefs.SetFloat(HACKING_BEST_SCORE, value);
        PlayerPrefs.Save();
    }

    public static ControlMethod GetHackingControlMethod()
    {
        ControlMethod defaultValue = ControlMethod.Swipe;
        if (!PlayerPrefs.HasKey(HACKING_CONTROL_METHOD))
        {
            SetHackingControlMethod(defaultValue);
        }

        return (ControlMethod)PlayerPrefs.GetInt(HACKING_CONTROL_METHOD);
    }

    public static void SetHackingControlMethod(ControlMethod value)
    {
        PlayerPrefs.SetInt(HACKING_CONTROL_METHOD, (int)value);
        PlayerPrefs.Save();
    }
}
