using UnityEngine;

public class WallJumpingGameManager : MonoBehaviour
{
    [Header("Layout weights")]
    public int uiLayoutWeight;
    public int gameAreaLayoutWeight;

    [Header("Game attributes")]
    public int wallHeightAdditional;
    public float playerForceScale;
    public float playerHeightToDie;

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
        InitializeWalls(weightSum, wallHeightAdditional);
        InitializePlayer();
    }

    /// <summary>
    /// Initializes UI.
    /// </summary>
    /// <param name="weightSum"></param>
    private void InitializeUi(float weightSum)
    {
        WallJumpingUiManager wallJumpingUiManager = FindObjectOfType<WallJumpingUiManager>();
        currentScore = 0;
        bestScore = PlayerPrefsWrapper.GetWallJumpingBestScore();
        float uiLayoutRatio = uiLayoutWeight / weightSum;
        wallJumpingUiManager.Initialize(currentScore, bestScore, uiLayoutRatio);
    }

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="weightSum"></param>
    private void InitializeWalls(float weightSum, int wallHeightAdditional)
    {
        GameArea gameArea = FindObjectOfType<GameArea>();
        float gameAreaHeightRatio = gameAreaLayoutWeight / weightSum;
        gameArea.Initialize(gameAreaHeightRatio, wallHeightAdditional);
    }

    private void InitializePlayer()
    {
        Player player = FindObjectOfType<Player>();
        player.Initialize(playerForceScale, playerHeightToDie);
    }
}
