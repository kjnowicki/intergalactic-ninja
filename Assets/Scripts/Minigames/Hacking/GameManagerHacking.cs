using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerHacking : MonoBehaviour
{
    #region singleton
    private static GameManagerHacking instance = null;

    public static GameManagerHacking Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManagerHacking>();

                if (instance == null)
                {
                    instance = new GameManagerHacking();
                }
            }
            return instance;
        }
    }
    #endregion

    private readonly HackingStringsModel stringsModel = StringsModelsModel.HackingStringsModel;
    private string bestScoreTemplate;
    private string currentScoreTemplate;
    private string timeTemplate;

    [Header("UI")]
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI timeText;
    public GameObject gravityArrow;
    public GameObject resetWindow;

    [Header("Game mechanics")]
    public float maxTime;
    private float currentTime;
    public float bubbleMultiplier;
    public float comboMultiplier;
    public float timeMultiplier;
    private float currentScore;
    private float bestScore;

    [Header("Prefabs")]
    public GameObject matrixPrefab;
    private GameObject matrixGameObject;
    public Matrix matrix;

    public BaseControlMethod controlMethod;
    public bool ControlEnabled { get; private set; }
    private const float ARROW_ANIMATION_SPEED = 0.1f;

    private void Awake()
    {
        InitializeData();
        controlMethod = ControlMethodFactory.GetControlMethod(matrix);
        BubbleAnimation.RandomIdleAnimation(matrix);
        resetWindow.SetActive(false);
    }

    /// <summary>
    /// Set everything needed at the begining
    /// </summary>
    private void InitializeData()
    {
        GetStrings();
        bestScore = PlayerPrefsWrapper.GetHackingBestScore();
        SetUI();
        ResetCurrentScore();
        matrixGameObject = Instantiate(matrixPrefab);
        matrix = matrixGameObject.GetComponent<Matrix>();
    }

    /// <summary>
    /// Get strings template.
    /// </summary>
    private void GetStrings()
    {
        bestScoreTemplate = stringsModel.BestScore;
        currentScoreTemplate = stringsModel.CurrentScore;
        timeTemplate = stringsModel.Time;
    }

    /// <summary>
    /// Set User Interface
    /// </summary>
    private void SetUI()
    {
        bestScoreText.text = String.Format(bestScoreTemplate, bestScore);
    }

    /// <summary>
    /// Start timer
    /// </summary>
    public void StartTimer()
    {
        currentTime = maxTime;
        EnableControl();
        StartCoroutine(Timer());
    }

    /// <summary>
    /// Time is ticking!
    /// </summary>
    public IEnumerator Timer()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeText.text = currentTime.ToString(timeTemplate);

            yield return null;
        }
        GameOver();
    }

    /// <summary>
    /// Calculate points.
    /// </summary>
    /// <param name="points"></param>
    /// <param name="combo"></param>
    public void AddCurrentScore(int points, int combo)
    {
        float pointsValue = points * bubbleMultiplier * combo * comboMultiplier;
        currentScore += pointsValue;
        currentScoreText.text = String.Format(currentScoreTemplate, currentScore.ToString());
        currentTime = currentTime > 0 ? currentTime + (pointsValue * timeMultiplier) : 0;
    }

    /// <summary>
    /// Set current time to 0 
    /// </summary>
    public void ResetCurrentScore()
    {
        currentScore = 0;
        currentScoreText.text = String.Format(currentScoreTemplate, currentScore.ToString());
    }

    /// <summary>
    /// Game over
    /// </summary>
    public void GameOver()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefsWrapper.SetHackingBestScoreAndSave(bestScore);
            bestScoreText.text = String.Format(bestScoreTemplate, bestScore);
        }
        ControlEnabled = false;
        resetWindow.SetActive(true);
    }

    /// <summary>
    /// Reset variables and controls. This is useed as button event.
    /// </summary>
    public void ResetGame()
    {
        resetWindow.SetActive(false);
        ResetCurrentScore();
        StartTimer();
        matrix.Reset();
        EnableControl();
    }

    /// <summary>
    /// Allows player to moves bubbles.
    /// </summary>
    public void EnableControl()
    {
        if (currentTime > 0)
            ControlEnabled = true;
    }
    /// <summary>
    /// Forbid player to moves bubbles.
    /// </summary>
    public void DisableControl()
    {
        ControlEnabled = false;
    }

    public async void RotateGravityArrow(Vector2 direction)
    {
        var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        while (Mathf.Abs(gravityArrow.transform.rotation.eulerAngles.z - targetRotation.eulerAngles.z) > ARROW_ANIMATION_SPEED)
        {
            gravityArrow.transform.rotation = Quaternion.Lerp(gravityArrow.transform.rotation, targetRotation, ARROW_ANIMATION_SPEED);
            await Task.Delay(1);
        }
        gravityArrow.transform.rotation = targetRotation;
    }

}
