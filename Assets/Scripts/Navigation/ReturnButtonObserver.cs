using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ReturnButtonObserver : MonoBehaviour
{
    public GameObject exitPopup;
    public TextMeshProUGUI exitText;
    public float exitTime;

    private readonly ReturnPopupStringsModel stringsModel = StringsModelsModel.ReturnStringsModel;

    private bool isExit = false;

    public UnityEvent returnButtonEvent;

    void Awake()
    {
        if (returnButtonEvent == null)
            returnButtonEvent = new UnityEvent();
    }

    private void Start()
    {
        exitText.SetText(stringsModel.ExitMessage);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isExit)
        {
            returnButtonEvent.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isExit = true;
            StartCoroutine(ReturnPopup());
        }

    }

    IEnumerator ReturnPopup()
    {
        exitPopup.SetActive(true);
        yield return new WaitForSecondsRealtime(exitTime);
        exitPopup.SetActive(false);
        isExit = false;
        yield return null;
    }

    public void LoadCocpitScene()
    {
        SceneManager.LoadScene("CockPitActionHub", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
