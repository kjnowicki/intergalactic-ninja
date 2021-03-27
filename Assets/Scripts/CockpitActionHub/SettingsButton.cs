using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject SettingsPopup;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsPopup.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        OpenSettingsPopup();
    }

    /// <summary>
    /// Opens settings popup.
    /// </summary>
    private void OpenSettingsPopup()
    {
        SettingsPopup.SetActive(true);
    }
}
