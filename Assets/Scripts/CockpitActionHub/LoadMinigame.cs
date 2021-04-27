using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMinigame : MonoBehaviour
{
    [HideInInspector]
    public int arrayIdx = 0;

    [HideInInspector]
    public string[] SceneNames = { "Killer", "Hacking", "NinjaHero" };
    public void LoadMinigameScene()
    {
        Debug.Log(string.Format("Loading scene: {0}...", SceneNames[arrayIdx]));
        SceneManager.LoadScene(SceneNames[arrayIdx], LoadSceneMode.Single);
    }
}
