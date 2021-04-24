using UnityEngine;

public class WallJumpingGameManager : MonoBehaviour
{
    [Header("Game attributes")]
    public int wallHeightAdditional;
    public float playerForceScale;
    public float garbageCollectorYCoord;
    public float slimeEffectDuration;
    public int maximumNumberOfJumps;
    public float playerInitialYPosition;

    [Header("References")]
    public GameObject gameReplayObject;

    WallJumpingUiManager wallJumpingUiManager;
    private SlimeGenerator slimeGenerator;
    private GameArea gameArea;
    private WallGenerator wallGenerator;
    private BackgroundGenerator backgroundGenerator;
    [SerializeField] private CameraRubberBand cameraMovement;

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
    /// Initializes the game.
    /// </summary>
    public void Initialize()
    {
        InitializePlayer();
    }

    public void Replay()
    {
        RestartUi();
        DisableEnvironmentMovement();
        gameArea.DestroyObjectsFromPreviousGame();
        gameArea.Restart();
        InitializePlayer();
        gameReplayObject.SetActive(false);
    }

    /// <summary>
    /// Initializes game parameters.
    /// </summary>
    private void SetUp()
    {
        DisableEnvironmentMovement();
        gameReplayObject.SetActive(false);
        wallJumpingUiManager = FindObjectOfType<WallJumpingUiManager>();
        slimeGenerator = FindObjectOfType<SlimeGenerator>();
        wallGenerator = FindObjectOfType<WallGenerator>();
        backgroundGenerator = FindObjectOfType<BackgroundGenerator>();
        InitializeUi();
        InitializeGameArea();
    }

    /// <summary>
    /// Initializes UI.
    /// </summary>
    private void InitializeUi()
    {
        float currentScore = 0.0f;
        float bestScore = PlayerPrefsWrapper.GetWallJumpingBestScore();
        wallJumpingUiManager.Initialize(currentScore, bestScore);
    }

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    private void InitializeGameArea()
    {
        gameArea = FindObjectOfType<GameArea>();
        gameArea.Initialize(this, wallHeightAdditional, garbageCollectorYCoord, slimeEffectDuration);
    }

    /// <summary>
    /// Initializes Player.
    /// </summary>
    private void InitializePlayer()
    {
        gameArea.InitializePlayer(playerForceScale, this);
    }

    private void RestartUi()
    {
        float currentScore = wallJumpingUiManager.CurrentScore;
        float bestScore = wallJumpingUiManager.BestScore;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefsWrapper.SetWallJupmingBestScoreAndSave((int)bestScore);
        }
        currentScore = 0.0f;
        wallJumpingUiManager.Restart(currentScore, bestScore);
    }

    /// <summary>
    /// Generates slime effect.
    /// </summary>
    public void GenerateSlimeEffect()
    {
        slimeGenerator.EnableSlimeEffect();
    }

    /// <summary>
    /// Adds score.
    /// </summary>
    /// <param name="score"></param>
    public void AddScore(float score)
    {
        wallJumpingUiManager.CurrentScore += score;
    }

    /// <summary>
    /// Invokes GameOver
    /// </summary>
    public void OnGameOver()
    {
        Time.timeScale = 0;
        slimeGenerator.DisableSlimeEffect();
        gameReplayObject.SetActive(true);
    }

    /// <summary>
    /// Generates new wall blocks form current scenario
    /// </summary>
    public void GenerateNewWallsOnTop()
    {
        wallGenerator.GenerateNextWallFromScenario();
    }

    /// <summary>
    /// Adds single background block on top.
    /// </summary>
    public void GenerateNewBackgroundOnTop()
    {
        backgroundGenerator.AddBackgroundBlockOnTop();
    }

    /// <summary>
    /// Enables camera movement game object
    /// </summary>
    public void EnableEnvironmentMovement()
    {
        cameraMovement.gameObject.SetActive(true);
    }

    /// <summary>
    /// Disables camera movement game object
    /// </summary>
    private void DisableEnvironmentMovement()
    {
        cameraMovement.gameObject.SetActive(false);
    }
}
