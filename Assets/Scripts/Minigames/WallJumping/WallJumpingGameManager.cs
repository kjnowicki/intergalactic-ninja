using UnityEngine;

public class WallJumpingGameManager : MonoBehaviour
{
    [Header("Layout weights")]
    public int uiLayoutWeight;
    public int gameAreaLayoutWeight;

    public float screenHeight;

    private WallJumpingUiManager wallJumpingUiManager;
    private float currentScore;
    private float bestScore;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
        GameObject uiLayout = wallJumpingUiManager.transform.Find("UILayout").gameObject;
        RectTransform uiLayoutTransform = uiLayout.GetComponent<RectTransform>();
        int sum = uiLayoutWeight + gameAreaLayoutWeight;
        screenHeight = Screen.height;
        float uiLayoutRatio = (uiLayoutWeight / (float)sum) * screenHeight;
        uiLayoutTransform.sizeDelta = new Vector2(0, uiLayoutRatio);
        //uiLayoutTransform.localPosition = new Vector3(0, , 0);
        uiLayoutTransform.anchoredPosition = new Vector3(0, -uiLayoutRatio, 0);
        //Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        //int weightsSum = uiLayoutWeight + gameAreaLayoutWeight;
    }

    /// <summary>
    /// Initializes game parameters.
    /// </summary>
    private void SetUp()
    {
        wallJumpingUiManager = FindObjectOfType<WallJumpingUiManager>();
        currentScore = 0;
        bestScore = PlayerPrefsWrapper.GetWallJumpingBestScore();
        wallJumpingUiManager.Initialize(currentScore, bestScore);
    }
}
