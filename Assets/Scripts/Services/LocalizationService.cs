using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationService
{
    /// <summary>
    /// Returns Locale instance from locale code.
    /// </summary>
    /// <param name="localeCode"></param>
    /// <returns></returns>
    public Locale GetLocaleByCode(string localeCode)
    {
        return LocalizationSettings.AvailableLocales.Locales.Find(it => it.Identifier.Code == localeCode);
    }

    /// <summary>
    /// Returns locale name in its language.
    /// </summary>
    /// <param name="locale"></param>
    /// <returns></returns>
    public string GetLocalizedLocaleName(Locale locale)
    {
        Dictionary<string, string> localeCodeToLocalizedNameDictionary = new Dictionary<string, string>()
        {
            {LocaleCodesModel.ENGLISH, "English"},
            {LocaleCodesModel.POLISH, "Polski"}
        };
        return localeCodeToLocalizedNameDictionary[locale.Identifier.Code];
    }

    /// <summary>
    /// Returns localized control method name
    /// </summary>
    /// <param name="controlMethod"></param>
    /// <returns></returns>
    public Task<string> GetLocalizedControlMethodName(ControlMethod controlMethod)
    {
        Dictionary<ControlMethod, string> controlMethodToStringKeyDictionary = new Dictionary<ControlMethod, string>()
        {
            {ControlMethod.Swipe, "SWIPE"},
            {ControlMethod.Click, "CLICK"}
        };
        string stringKey = controlMethodToStringKeyDictionary[controlMethod];
        return LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Strings", stringKey).Task;
    }
}