using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Localization.Components;

public class NinjaHeroGameManager : MonoBehaviour
{
    #region singleton
    private static NinjaHeroGameManager instance = null;

    public static NinjaHeroGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NinjaHeroGameManager>();

                if (instance == null)
                {
                    instance = new NinjaHeroGameManager();
                }
            }
            return instance;
        }
    }

    #endregion

    private PaternTilesContainerManager paternTilesContainerManager;

    public bool isGameOver;
    public GameObject GameOverUI;
    private CancellationTokenSource cancellationToken = new CancellationTokenSource();
    private NinjaHeroGarbageCollector garbageCollector;
    public int ElementsToBeClicked { get; set; }

    [Header("UI")]
    public LocalizeStringEvent currentScoreStringEvent;
    public LocalizeStringEvent bestScoreStringEvent;

    private int currentScore = 0;
    public int bestScore;

    [Header("Game speed up")]
    public float speedUpLimit;
    public float speed;
    private float initialSpeed;
    public float speedUpValue;
    public float speedUpTime;
    private float initialSpeedUpTime;

    [Header("Balance game parametrs")]
    [SerializeField] private int paternTileCount;
    [SerializeField] private float spawnMinInterval;
    [SerializeField] private float spawnMaxInterval;
    [SerializeField] private int sliderMinLength;
    [SerializeField] private int sliderMaxLength;
    [SerializeField] private int maxElementsToBeClicked;
    public int MaxElementsToBeClicked => maxElementsToBeClicked;

    public int SliderMinLength { get => sliderMinLength; private set => sliderMinLength = value; }
    public int SliderMaxLength { get => sliderMaxLength; private set => sliderMaxLength = value; }
    IEnumerator speedUpIEnumerator;
    public int CurrentScore
    {
        get => currentScore;
        private set
        {
            currentScore = value;
            currentScoreStringEvent.RefreshString();
        }
    }

    public int BestScore
    {
        get => bestScore;
        private set
        {
            bestScore = value;
            bestScoreStringEvent.RefreshString();
        }
    }

    void Awake()
    {
        initialSpeed = speed;
        initialSpeedUpTime = speedUpTime;
        speedUpIEnumerator = SpeedUp();
        StartCoroutine(speedUpIEnumerator);
        paternTilesContainerManager = GetComponentInChildren<PaternTilesContainerManager>();
        paternTilesContainerManager.InitializeData(paternTileCount, spawnMinInterval, spawnMaxInterval);
        bestScore = PlayerPrefsWrapper.GetNinjaHeroBestScore();
    }

    private void Start()
    {

        paternTilesContainerManager.StartSpawn(cancellationToken.Token);
        garbageCollector = FindObjectOfType<NinjaHeroGarbageCollector>();
        garbageCollector.cancellationToken = cancellationToken.Token;
    }

    public void OnStart()
    {
        StartCoroutine(SpeedUp());
    }

    private void Update()
    {
        TouchHandler.HandleTouches();
    }

    /// <summary>
    /// Method to get list of all spawners
    /// </summary>
    /// <returns></returns>
    public List<Spawner> GetSpawners()
    {
        return paternTilesContainerManager.GetSpawners();
    }


    /// <summary>
    /// To kill all async thread
    /// </summary>
    private void OnDestroy()
    {
        cancellationToken.Cancel();
        cancellationToken.Dispose();
        Time.timeScale = 1;
    }

    /// <summary>
    /// Makes the game harder by speeding up elements
    /// </summary>
    /// <return>Corutine</return>>
    IEnumerator SpeedUp()
    {
        while (speedUpLimit >= Time.timeScale)
        {
            yield return new WaitForSeconds(speedUpTime);
            Time.timeScale *= speedUpValue;
            speedUpTime *= speedUpValue;
        }
        Time.timeScale = speedUpLimit;
        yield return null;
    }
    /// <summary>
    /// Stops game, destroys all elements on scene and activates game over object
    /// </summary>
    public void GameOver()
    {
        StopCoroutine(speedUpIEnumerator);
        cancellationToken.Cancel();

        Time.timeScale = 0;
        foreach (Element element in GetSpawners().SelectMany(x => x.GetComponentsInChildren<Element>()))
        {
            Destroy(element.gameObject);
        }
        SaveScore();

        GameOverUI.SetActive(true);
    }
    /// <summary>
    /// Increments UI score
    /// </summary>
    public void IncrementScore()
    {
        CurrentScore += 1;
    }
    /// <summary>
    /// Reset spawners and cancellation token
    /// </summary>
    public void ResetGame()
    {
        speed = initialSpeed;
        speedUpTime = initialSpeedUpTime;
        ElementsToBeClicked = 0;
        cancellationToken.Dispose();
        cancellationToken = new CancellationTokenSource();
        garbageCollector.cancellationToken = cancellationToken.Token;
        GameOverUI.SetActive(false);
        paternTilesContainerManager.StartSpawn(cancellationToken.Token);
        Time.timeScale = 1f;
        isGameOver = false;
    }

    public void SaveScore()
    {
        if (CurrentScore > BestScore)
        {
            BestScore = CurrentScore;
            PlayerPrefsWrapper.SetNinjaHeroBestScore(BestScore);
        }
        CurrentScore = 0;
    }
}
