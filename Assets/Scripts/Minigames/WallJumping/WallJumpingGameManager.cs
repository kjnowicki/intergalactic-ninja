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
    public float downwardForce;
    [Header("")]
    public GameObject movableObjectsColection;

    private float currentScore;
    private float bestScore;


    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    private void Update()
    {
        CameraMovement();
    }

    /// <summary>
    /// Initializes game parameters.
    /// </summary>
    private void SetUp()
    {
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

    /// <summary>
    /// Initializes Player.
    /// </summary>
    private void InitializePlayer()
    {
        WallJumpingPlayer player = FindObjectOfType<WallJumpingPlayer>();
        player.Initialize(playerForceScale);
    }

    private void CameraMovement()
    {
        float deltaTime = Time.deltaTime;
        float moveValue = downwardForce * deltaTime;
        foreach (Transform movablePosition in movableObjectsColection.GetComponentsInChildren<Transform>())
        {
            if (movablePosition.parent == movableObjectsColection.transform)
                movablePosition.position += Vector3.down * moveValue;
        }
    }
}
