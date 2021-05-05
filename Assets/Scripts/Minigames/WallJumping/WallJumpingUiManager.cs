using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class WallJumpingUiManager : MonoBehaviour
{
    private static WallJumpingStringsModel stringsModel;
    private float currentScore;
    private float bestScore;

    [Header("References")]
    public LocalizeStringEvent currentScoreStringEvent;
    public LocalizeStringEvent bestScoreStringEvent;

    public float CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            currentScoreStringEvent.RefreshString();
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
            bestScoreStringEvent.RefreshString();
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
    }
}