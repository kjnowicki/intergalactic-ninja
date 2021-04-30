using TMPro;
using UnityEngine;

public class NinjaHeroUiManager : MonoBehaviour
{
    private NinjaHeroStringsModel stringsModel;
    private int currentScore;
    private int bestScore;
    private string currentScoreTemplate;
    private string bestScoreTemplate;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;

    public int CurrentScore
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

    public int BestScore
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

    /// <summary>
    /// Initializes UI manager
    /// </summary>
    /// <param name="currentScore"></param>
    /// <param name="bestScore"></param>
    public void Initialize()
    {
        stringsModel = StringsModelsModel.NinjaHeroStringsModel;
        currentScoreTemplate = stringsModel.CurrentScore;
        bestScoreTemplate = stringsModel.BestScore;
        CurrentScore = 0;
        BestScore = PlayerPrefsWrapper.GetNinjaHeroBestScore();
    }
}
