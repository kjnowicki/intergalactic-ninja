using TMPro;
using UnityEngine;

public class WallJumpingUiManager : MonoBehaviour
{
    private static WallJumpingStringsModel stringsModel;
    private float currentScore;
    private float bestScore;
    private string currentScoreTemplate;
    private string bestScoreTemplate;

    [Header("References")]
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject uiContainer;

    public float CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            currentScoreText.SetText(string.Format(currentScoreTemplate, value.ToString("0000")));
        }
    }

    public float BestScore
    {
        get
        {
            return bestScore;
        }
        set
        {
            bestScore = value;
            bestScoreText.SetText(string.Format(bestScoreTemplate, value.ToString("0000")));
        }
    }

    /// <summary>
    /// Initializes UI.
    /// </summary>
    /// <param name="initialCurrentScore"></param>
    /// <param name="initialBestScore"></param>
    /// <param name="uiLayoutRatio"></param>
    public void Initialize(float initialCurrentScore, float initialBestScore, float uiLayoutRatio)
    {
        InitializePosition(uiLayoutRatio);
        InitializeText(initialCurrentScore, initialBestScore);
    }

    /// <summary>
    /// Initializes UI position.
    /// </summary>
    /// <param name="uiLayoutRatio"></param>
    private void InitializePosition(float uiLayoutRatio)
    {
        RectTransform uiLayoutTransform = uiContainer.GetComponent<RectTransform>();
        float fixedUiLayoutHeight = uiLayoutRatio * Screen.height;
        uiLayoutTransform.sizeDelta = new Vector2(0, fixedUiLayoutHeight);
    }

    /// <summary>
    /// Initializes text templates and values.
    /// </summary>
    /// <param name="initialCurrentScore"></param>
    /// <param name="initialBestScore"></param>
    private void InitializeText(float initialCurrentScore, float initialBestScore)
    {
        stringsModel = StringsModelsModel.WallJumpingStringsModel;
        currentScoreTemplate = stringsModel.CurrentScore;
        bestScoreTemplate = stringsModel.BestScore;
        CurrentScore = initialCurrentScore;
        BestScore = initialBestScore;
    }
}
