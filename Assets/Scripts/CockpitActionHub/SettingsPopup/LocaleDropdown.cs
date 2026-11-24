using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocaleDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private Locale currentLocale;
    private readonly LocalizationService localizationService = new LocalizationService();
    private HackingControlDropdown hackingControlDropdown;

    IEnumerator Start()
    {
        hackingControlDropdown = FindAnyObjectByType<HackingControlDropdown>();
        // Wait for the localization system to initialize, loading Locales, preloading etc.
        yield return LocalizationSettings.InitializationOperation;
        SetState();
        dropdown.onValueChanged.AddListener(LocaleSelected);
    }

    /// <summary>
    /// Sets Locale dropdown state.
    /// </summary>
    private void SetState()
    {
        // Generate list of available Locales
        var options = new List<TMP_Dropdown.OptionData>();
        int selected = 0;
        currentLocale = LocalizationSettings.SelectedLocale;
        for(int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            if (currentLocale == locale)
                selected = i;
            options.Add(new TMP_Dropdown.OptionData(localizationService.GetLocalizedLocaleName(locale)));
        }
        dropdown.options = options;
        dropdown.value = selected;
    }
    
    /// <summary>
    /// Sets selected locale if dropdown value changed.
    /// </summary>
    /// <param name="index"></param>
    private async void LocaleSelected(int index)
    {
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        if (selectedLocale != currentLocale)
        {
            currentLocale = selectedLocale;
            PlayerPrefsWrapper.SetLocaleAndSave(selectedLocale);
            LocalizationSettings.SelectedLocale = selectedLocale;
            await OnLocaleChanged();
        }
    }

    /// <summary>
    /// Performs actions happening when locale is changed.
    /// </summary>
    /// <returns></returns>
    private async Task OnLocaleChanged()
    {
        await hackingControlDropdown.SetState();
    }
}