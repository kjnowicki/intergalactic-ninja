using UnityEngine;

public class WallJumpingGameManager : MonoBehaviour
{
    [Header("Layout weights")]
    public int uiLayoutWeight;
    public int gameAreaLayoutWeight;

    [Header("Game attributes")]
    public int wallHeightAdditional;

    public GameObject leftWall;
    public GameObject rightWall;

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
        InitializeWalls(weightSum, wallHeightAdditional);
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
    private void InitializeWalls(float weightSum, int wallHeightAdditional)
    {
        wallGenerator = FindObjectOfType<WallGenerator>();
        float gameAreaHeightRatio = gameAreaLayoutWeight / weightSum;
        wallGenerator.Initialize(gameAreaHeightRatio, wallHeightAdditional);
    }

    public void ActivateLeftWallColiders()
    {
        var walls = leftWall.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D childColider in walls)
        {
            childColider.enabled = true;
        }
    }

    public void ActivateRightWallColiders()
    {
        var walls = rightWall.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D childColider in walls)
        {
            childColider.enabled = true;
        }
    }
}
