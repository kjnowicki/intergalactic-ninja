using TMPro;
using UnityEngine;

public class WallJumpingUiManager : MonoBehaviour
{
    private static WallJumpingStringsModel stringsModel;
    private float currentScore;
    private float bestScore;
    private string currentScoreTemplate;
    private string bestScoreTemplate;

    [Header("UI score display")]
    public TextMeshPro currentScoreText;
    public TextMeshPro bestScoreText;

    public float CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            currentScoreText.SetText(string.Format(currentScoreTemplate, value));
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
            bestScoreText.SetText(string.Format(bestScoreTemplate, value));
        }
    }

    public void Initialize(float currentScore, float bestScore)
    {
        stringsModel = StringsModelsModel.WallJumpingStringsModel;
        currentScoreTemplate = stringsModel.CurrentScore;
        bestScoreTemplate = stringsModel.BestScore;
        CurrentScore = currentScore;
        BestScore = bestScore;
    }
}
