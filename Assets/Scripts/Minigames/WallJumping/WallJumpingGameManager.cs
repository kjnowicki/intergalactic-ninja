using UnityEngine;

public class WallJumpingGameManager : MonoBehaviour
{
    public WallJumpingUiManager wallJumpingUiManager;

    private int currentScore;
    private int bestScore;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        currentScore = 0;
        bestScore = PlayerPrefsWrapper.GetWallJumpingBestScore();
        wallJumpingUiManager.CurrentScore = currentScore;
        wallJumpingUiManager.BestScore = bestScore;
    }
}
