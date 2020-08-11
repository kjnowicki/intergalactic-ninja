using UnityEngine;

public class WallJumpingGameManager : MonoBehaviour
{
    public WallJumpingUiManager wallJumpingUiManager;

    private float currentScore;
    private float bestScore;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    /// <summary>
    /// Initializes game parameters.
    /// </summary>
    private void SetUp()
    {
        currentScore = 0;
        bestScore = PlayerPrefsWrapper.GetWallJumpingBestScore();
        wallJumpingUiManager.Initialize(currentScore, bestScore);
    }
}
