using System;
using UnityEngine;
using UnityEngine.Events;

public class StartGame : MonoBehaviour
{
    private const float STARTING_TIMESCALE = 1f;

    public UnityEvent onSceneLoadedAction;
    public UnityEvent onStartClickedAction;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        onSceneLoadedAction?.Invoke();
    }

    /// <summary>
    /// What happens Start button is clicked.
    /// This method should be overriden if anything else or something different should happen in a minigame. 
    /// </summary>
    public virtual void OnStartButtonClicked()
    {
        Time.timeScale = STARTING_TIMESCALE;
        Destroy(gameObject);
        onStartClickedAction?.Invoke();
    }
}
