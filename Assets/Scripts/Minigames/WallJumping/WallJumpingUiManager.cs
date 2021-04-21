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
    public void Initialize(float initialCurrentScore, float initialBestScore)
    {
        InitializeText();
        InitializeValues(initialCurrentScore, initialBestScore);
    }

    public void Restart(float initialCurrentScore, float initialBestScore)
    {
        InitializeValues(initialCurrentScore, initialBestScore);
    }

    /// <summary>
    /// Initializes values.
    /// </summary>
    /// <param name="initialCurrentScore"></param>
    /// <param name="initialBestScore"></param>
    private void InitializeValues(float initialCurrentScore, float initialBestScore)
    {
        CurrentScore = initialCurrentScore;
        BestScore = initialBestScore;
    }

    /// <summary>
    /// Initializes text templates.
    /// </summary>
    private void InitializeText()
    {
        stringsModel = StringsModelsModel.WallJumpingStringsModel;
        currentScoreTemplate = stringsModel.CurrentScore;
        bestScoreTemplate = stringsModel.BestScore;
    }
}