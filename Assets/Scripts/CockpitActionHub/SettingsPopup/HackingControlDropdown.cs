using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class HackingControlDropdown : MonoBehaviour
{
    private static readonly List<ControlMethod> AvailableControlMethods = new List<ControlMethod>
    {
        ControlMethod.Click,
        ControlMethod.Swipe
    };
    private readonly LocalizationService localizationService = new LocalizationService();
    private ControlMethod currentControlMethod;

    public TMP_Dropdown dropdown;

    async void Start()
    {
        currentControlMethod = PlayerPrefsWrapper.GetHackingControlMethod();
        await SetState();
        dropdown.onValueChanged.AddListener(ControlMethodSelected);
    }

    /// <summary>
    /// Sets Hacking control method dropdown state.
    /// </summary>
    /// <returns></returns>
    public async Task SetState()
    {
        var options = new List<TMP_Dropdown.OptionData>();
        int selected = 0;
        for (int i = 0; i < AvailableControlMethods.Count; ++i)
        {
            ControlMethod controlMethod = AvailableControlMethods[i];
            var localizedControlMethodName = await localizationService.GetLocalizedControlMethodName(controlMethod);
            if (currentControlMethod == controlMethod)
                selected = i;
            options.Add(new TMP_Dropdown.OptionData(localizedControlMethodName));
        }
        dropdown.options = options;
        dropdown.value = selected;
    }

    /// <summary>
    /// Sets Hacking control method if dropdown value changed.
    /// </summary>
    /// <param name="index"></param>
    void ControlMethodSelected(int index)
    {
        ControlMethod selectedControlMethod = AvailableControlMethods[index];
        if (selectedControlMethod != currentControlMethod)
        {
            currentControlMethod = selectedControlMethod;
            PlayerPrefsWrapper.SetHackingControlMethod(selectedControlMethod);
        }
    }
}