using TMPro;
using UnityEngine;

public class WallJumpingUiManager : MonoBehaviour
{
    private static WallJumpingStringsModel stringsModel = StringsModelsModel.WallJumpingStringsModel;
    private int currentScore;
    private int bestScore;

    [Header("UI score display")]
    public TextMeshPro currentScoreText;
    public TextMeshPro bestScoreText;

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            currentScoreText.SetText(string.Format(stringsModel.CurrentScore, value));
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
            bestScoreText.SetText(string.Format(stringsModel.BestScore, value));
        }
    }
}
