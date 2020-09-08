using UnityEngine;

public class StartGame : MonoBehaviour
{
    const float STARTING_TIMESCALE = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Starting the game. Resume time and destroy starting pop-up.
    /// </summary>
    public void GameStart()
    {
        Time.timeScale = STARTING_TIMESCALE;
        Destroy(gameObject);
    }
}
