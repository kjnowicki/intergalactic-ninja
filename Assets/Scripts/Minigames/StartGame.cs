using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private const float STARTING_TIMESCALE = 1f;

    public UnityEvent onStartClickedAction;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
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

    public virtual void OnBackButtonClicked()
    {
        SceneManager.LoadScene("CockPitActionHub", LoadSceneMode.Single);
    }
}