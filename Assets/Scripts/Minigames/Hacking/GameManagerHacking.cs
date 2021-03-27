using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Components;

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

    [Header("UI")]
    public LocalizeStringEvent currentScoreStringEvent;
    public LocalizeStringEvent bestScoreStringEvent;
    public LocalizeStringEvent timeLeftStringEvent;
    public GameObject gravityArrow;
    public GameObject resetWindow;
    

    [Header("Game mechanics")]
    public float maxTime;
    private float timeLeft;
    public float comboMultiplier;
    public float timeMultiplier;
    private float currentScore;
    private float bestScore;

    public float CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore = value;
            currentScoreStringEvent.RefreshString();
        }
    }
    
    public float BestScore
    {
        get => bestScore;
        set
        {
            bestScore = value;
            bestScoreStringEvent.RefreshString();
        }
    }
    
    public float TimeLeft
    {
        get => timeLeft;
        set
        {
            timeLeft = value;
            timeLeftStringEvent.RefreshString();
        }
    }

    [Header("Prefabs")]
    public GameObject matrixPrefab;
    private GameObject matrixGameObject;
    public Matrix matrix;

    public BaseControlMethod controlMethod;
    public bool ControlEnabled { get; private set; }

    [Header("Gravity arrow")]
    [SerializeField] private float arrowAnimationSpeed = 10f;
    private Quaternion prevArrowRotation;
    private Coroutine gravityArrowCoroutine;

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
        BestScore = PlayerPrefsWrapper.GetHackingBestScore();
        ResetCurrentScore();
        matrixGameObject = Instantiate(matrixPrefab);
        matrix = matrixGameObject.GetComponent<Matrix>();
    }

    /// <summary>
    /// Start timer
    /// </summary>
    public void StartTimer()
    {
        TimeLeft = maxTime;
        EnableControl();
        StartCoroutine(Timer());
    }

    /// <summary>
    /// Time is ticking!
    /// </summary>
    public IEnumerator Timer()
    {
        while (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            yield return null;
        }
        Task gameOverTask = GameOver();
        while (!gameOverTask.IsCompleted) yield return null;
    }

    /// <summary>
    /// Calculate points.
    /// </summary>
    /// <param name="points"></param>
    /// <param name="combo"></param>
    public void AddCurrentScore(int points, int combo)
    {
        float pointsValue = points  * combo * comboMultiplier;
        CurrentScore += pointsValue;
        TimeLeft = TimeLeft > 0 ? TimeLeft + (pointsValue * timeMultiplier) : 0;
    }

    /// <summary>
    /// Set current time to 0 
    /// </summary>
    public void ResetCurrentScore()
    {
        CurrentScore = 0;
    }

    /// <summary>
    /// Game over
    /// </summary>
    public async Task GameOver()
    {
        while (!ControlEnabled)
            await Task.Delay(1);
        DisableControl();

        if (currentScore > bestScore)
        {
            BestScore = CurrentScore;
            PlayerPrefsWrapper.SetHackingBestScoreAndSave(BestScore);
        }
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
        ControlEnabled = true;
    }
    /// <summary>
    /// Forbid player to moves bubbles.
    /// </summary>
    public void DisableControl()
    {
        ControlEnabled = false;
    }

    /// <summary>
    /// It stops previous rotation and start new one
    /// </summary>
    /// <param name="direction"></param>
    public void RotateGravityArrow(Vector2 direction)
    {
        if (gravityArrowCoroutine != null)
            StopCoroutine(gravityArrowCoroutine);
        gravityArrow.transform.rotation = prevArrowRotation;
        gravityArrowCoroutine = StartCoroutine(StartRotateGravityArrow(direction));
    }

    /// <summary>
    /// coroutine which rotate arrow
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    IEnumerator StartRotateGravityArrow(Vector2 direction)
    {
        var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        prevArrowRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        while (Mathf.Abs(gravityArrow.transform.rotation.eulerAngles.z - prevArrowRotation.eulerAngles.z) > arrowAnimationSpeed)
        {
            gravityArrow.transform.rotation = Quaternion.Lerp(gravityArrow.transform.rotation, prevArrowRotation, Time.deltaTime * arrowAnimationSpeed);
            yield return null;
        }
        gravityArrow.transform.rotation = prevArrowRotation;
    }
}
