using UnityEngine;

public class WallJumpingGameManager : MonoBehaviour
{
    [Header("Layout weights")]
    public int uiLayoutWeight;
    public int gameAreaLayoutWeight;

    private WallJumpingUiManager wallJumpingUiManager;
    private WallGenerator wallGenerator;
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
        float weightSum = uiLayoutWeight + gameAreaLayoutWeight;
        InitializeUi(weightSum);
        InitializeWalls(weightSum);
    }

    /// <summary>
    /// Initializes UI.
    /// </summary>
    /// <param name="weightSum"></param>
    private void InitializeUi(float weightSum)
    {
        wallJumpingUiManager = FindObjectOfType<WallJumpingUiManager>();
        currentScore = 0;
        bestScore = PlayerPrefsWrapper.GetWallJumpingBestScore();
        float uiLayoutRatio = uiLayoutWeight / weightSum;
        wallJumpingUiManager.Initialize(currentScore, bestScore, uiLayoutRatio);
    }

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="weightSum"></param>
    private void InitializeWalls(float weightSum)
    {
        wallGenerator = FindObjectOfType<WallGenerator>();
        float gameAreaHeightRatio = gameAreaLayoutWeight / weightSum;
        wallGenerator.Initialize(gameAreaHeightRatio);
    }
}
