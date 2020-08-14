using UnityEngine;

public class WallJumpingGameManager : MonoBehaviour
{
    [Header("Layout weights")]
    public int uiLayoutWeight;
    public int gameAreaLayoutWeight;

    private WallJumpingUiManager wallJumpingUiManager;
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
        InitializeUi();
        //InitializeWalls();
    }

    private void InitializeUi()
    {
        wallJumpingUiManager = FindObjectOfType<WallJumpingUiManager>();

        currentScore = 0;
        bestScore = PlayerPrefsWrapper.GetWallJumpingBestScore();

        float weightSum = uiLayoutWeight + gameAreaLayoutWeight;
        float uiLayoutRatio = uiLayoutWeight / weightSum;
        
        wallJumpingUiManager.Initialize(currentScore, bestScore, uiLayoutRatio);
    }
}
