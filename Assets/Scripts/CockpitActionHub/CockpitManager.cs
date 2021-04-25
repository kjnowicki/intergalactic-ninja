using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class CockpitManager : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return LoadLanguage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Loads language saved in PlayerPrefs as current game language.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadLanguage()
    {
        yield return LocalizationSettings.InitializationOperation;
        Locale locale = PlayerPrefsWrapper.GetLocale();
        LocalizationSettings.SelectedLocale = locale;
    }
}
