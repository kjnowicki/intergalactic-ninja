using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesButton : MonoBehaviour
{
    public GameObject MinigamesPopup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MinigamesPopup.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        OpenMinigamesPopup();
    }

    private void OpenMinigamesPopup()
    {
        Debug.Log("Activating minigamaes popup");
        MinigamesPopup.SetActive(true);
    }
}
