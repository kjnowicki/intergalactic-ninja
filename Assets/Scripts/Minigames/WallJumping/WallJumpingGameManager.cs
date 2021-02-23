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
    public float slimeEffectDuration;

    private float currentScore;
    private float bestScore;
    WallJumpingUiManager wallJumpingUiManager;
    private SlimeGenerator slimeGenerator;
    private WallGenerator wallGenerator;

    #region singleton
    private static WallJumpingGameManager instance;

    public static WallJumpingGameManager Instance { get { return instance; } }

    private void CheckForInstanceDuplicate()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion
    private void Awake()
    {
        CheckForInstanceDuplicate();
    }


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
        wallJumpingUiManager = FindObjectOfType<WallJumpingUiManager>();
        slimeGenerator = FindObjectOfType<SlimeGenerator>();
        wallGenerator = FindObjectOfType<WallGenerator>();
        float weightSum = uiLayoutWeight + gameAreaLayoutWeight;
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
        gameArea.Initialize(gameAreaHeightRatio, wallHeightAdditional, garbageCollectorYCoord, slimeEffectDuration);
    }

    /// <summary>
    /// Initializes Player.
    /// </summary>
    private void InitializePlayer()
    {
        WallJumpingPlayer player = FindObjectOfType<WallJumpingPlayer>();
        player.Initialize(playerForceScale, this);
    }

    /// <summary>
    /// Generates slime effect.
    /// </summary>
    public void GenerateSlimeEffect()
    {
        slimeGenerator.EnableSlimeEffect();
    }

    public void GenerateNewWallsOnTop()
    {
        wallGenerator.GenerateNextWallFromScenario();
    }
}
