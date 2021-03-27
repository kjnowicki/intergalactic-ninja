using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Components;

public class GameManagerKiller : MonoBehaviour
{
    #region singleton
    private static GameManagerKiller instance = null;

    public static GameManagerKiller Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManagerKiller>();

                if (instance == null)
                {
                    instance = new GameManagerKiller();
                }
            }
            return instance;
        }
    }
    #endregion

    private float spawnTimer;
    private float lifeTime;
    private IEnumerator spawnTargetCoroutine;
    private IEnumerator speedUpCoroutine;

    [Header("UI score display")]
    public LocalizeStringEvent currentScoreStringEvent;
    public LocalizeStringEvent bestScoreStringEvent;
    private int currentScore;
    private int bestScore;

    [Header("Target")]
    public Target target;
    public string targetTag;
    public GameObject targetContainer;

    [Header("Target parameters")]
    public float initialSpawnTimer;
    public float initialLifeTime;
    [Range(0.0f, 1.0f)]
    public float lowerLifeTime;
    [Range(0.0f, 1.0f)]
    public float speedUpSpawn;
    public float speedUpTime;

    [Header("Spawn area borders")]
    [Range(0.0f, 1.0f)]
    public float topScreenBorder;
    [Range(0.0f, 1.0f)]
    public float bottomScreenBorder;
    [Range(0.0f, 1.0f)]
    public float leftScreenBorder;
    [Range(0.0f, 1.0f)]
    public float rightScreenBorder;

    [Header("Game over flag")]
    public bool gameOver;
    [Header("Game over UI object")]
    public GameObject gameOverObejct;
    //TO DO:
    //Exp progres bar

    public int CurrentScore
    {
        get => currentScore;
        set
        {
            currentScore = value;
            currentScoreStringEvent.RefreshString();
        }
    }
    
    public int BestScore
    {
        get => bestScore;
        set
        {
            bestScore = value;
            bestScoreStringEvent.RefreshString();
        }
    }

    public void Initialize()
    {
        InitializeData();

        StartCoroutine(spawnTargetCoroutine);
        StartCoroutine(speedUpCoroutine);
    }
    
    /// <summary>
    /// Initializes data on scene startup
    /// </summary>
    private void InitializeData()
    {
        gameOverObejct.SetActive(false);
        gameOver = false;
        spawnTimer = initialSpawnTimer;
        lifeTime = initialLifeTime;
        Time.timeScale = 1;

        CurrentScore = 0;
        BestScore = PlayerPrefsWrapper.GetKillerBestScore();

        spawnTargetCoroutine = SpawnTarget();
        speedUpCoroutine = SpeedUp();
    }
    
    /// <summary>
    /// Makes the game harder by speeding up spawn time and lowering life time of targets
    /// </summary>
    /// <return>Corutine</return>>
    IEnumerator SpeedUp()
    {
        while(!gameOver)
        {
            lifeTime *= lowerLifeTime;
            spawnTimer *= speedUpSpawn;
            yield return new WaitForSeconds(speedUpTime);
        }
        yield return null;
    }
    
    /// <summary>
    /// Spawns next target while gameOver flag is set to false
    /// </summary>
    /// <return>Corutine</return>>
    IEnumerator SpawnTarget()
    {
        float topSpawnBorder = Screen.height * topScreenBorder;
        float bottomSpawnBorder = Screen.height * bottomScreenBorder;
        float leftSpawnBorder = Screen.width * leftScreenBorder;
        float rightSpawnBorder = Screen.width * rightScreenBorder;
        while (!gameOver)
        {
            float spawnAreaWidth = UnityEngine.Random.Range(leftSpawnBorder, rightSpawnBorder);
            float spawnAreaHeight = UnityEngine.Random.Range(topSpawnBorder, bottomSpawnBorder);
            Vector2 targetSpawnPosition = Camera.main.ScreenToWorldPoint(new Vector2( spawnAreaWidth, spawnAreaHeight));
            Target newTarget = Instantiate(target, targetSpawnPosition, Quaternion.identity, targetContainer.transform);          
            newTarget.LifeTime = lifeTime;
            yield return new WaitForSeconds(spawnTimer);
        }
        yield return null;
    }

    /// <summary>
    /// GameOver sets gameOver flag to true and makes Game Over UI visible
    /// </summary>
    public void GameOver()
    {
        gameOverObejct.SetActive(true);
        gameOver = true;
        DestroyAllTargetsOnScene();

        if (CurrentScore > BestScore)
        {
            PlayerPrefsWrapper.SetKillerBestScoreAndSave(CurrentScore);
        }
        
    }
    
    /// <summary>
    /// Destroys all targets on scene
    /// </summary>
    private void DestroyAllTargetsOnScene()
    {
        foreach (var target in GameObject.FindGameObjectsWithTag(targetTag))
        {
            Destroy(target);
        }
    }

    /// <summary>
    /// Reloads the scene
    /// </summary>
    public void Replay()
    {
        StopCoroutine(spawnTargetCoroutine);
        StopCoroutine(speedUpCoroutine);
        Initialize();
    }
}
