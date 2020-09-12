using UnityEngine;

public class WallJumpingGameManager : MonoBehaviour
{
    [Header("Layout weights")]
    public int uiLayoutWeight;
    public int gameAreaLayoutWeight;

    [Header("Game attributes")]
    public int wallHeightAdditional;
    public float playerForceScale;
    public float garbageCollectorYCoord;

    private float currentScore;
    private float bestScore;
    private WallGenerator wallGenerator;

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
        wallGenerator = GetComponentInChildren<WallGenerator>();
        InitializeUi(weightSum);
        InitializeGameArea(weightSum);
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
    private void InitializeGameArea(float weightSum)
    {
        GameArea gameArea = FindObjectOfType<GameArea>();
        float gameAreaHeightRatio = gameAreaLayoutWeight / weightSum;
        gameArea.Initialize(gameAreaHeightRatio, wallHeightAdditional, garbageCollectorYCoord);
    }

    private void InitializePlayer()
    {
        WallJumpingPlayer player = FindObjectOfType<WallJumpingPlayer>();
        player.Initialize(playerForceScale, this);
    }

    //public void ActivateLeftWallColiders()
    //{
    //    wallGenerator.
    //    var walls = leftWall.GetComponentsInChildren<Collider2D>();
    //    foreach (Collider2D childColider in walls)
    //    {
    //        childColider.enabled = true;
    //    }
    //}

    //public void ActivateRightWallColiders()
    //{
    //    var walls = rightWall.GetComponentsInChildren<Collider2D>();
    //    foreach (Collider2D childColider in walls)
    //    {
    //        childColider.enabled = true;
    //    }
    //}
}
