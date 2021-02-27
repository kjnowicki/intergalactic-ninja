using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ReplayGame : MonoBehaviour
{
    private const float STARTING_TIMESCALE = 1f;
    
    public UnityEvent onReplayClickedAction;
    public UnityEvent onCancelClickedAction;

    /// <summary>
    /// What happens Replay button is clicked.
    /// This method should be overriden if anything else or something different should happen in a minigame. 
    /// </summary>
    public virtual void OnReplayButtonClicked()
    {
        Time.timeScale = STARTING_TIMESCALE;
        onReplayClickedAction?.Invoke();
    }

    /// <summary>
    /// What happens Cancel button is clicked.
    /// This method should be overriden if anything else or something different should happen in a minigame.
    /// </summary>
    public virtual void OnCancelButtonClicked()
    {
        SceneManager.LoadScene("CockPitActionHub", LoadSceneMode.Single);
        onCancelClickedAction?.Invoke();
    }
}
