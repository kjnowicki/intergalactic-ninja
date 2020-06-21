using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private readonly KillerStringsModel _stringsModel = StringsModelsModel.KillerStringsModel;
    private string _bestScoreTemplate;
    private string _currentScoreTemplate;

    [Header("UI score display")]
    public TextMeshPro currentScoreText;
    private int _currentScore;
    public TextMeshPro bestScoreText;

    [Header("Target")]
    public Target target;
    public string targetTag;
    public GameObject targetContainer;

    [Header("Target parameters")]
    public float spawnTimer;
    public float lifeTime;
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

    void Start()
    {
        InitializeData();

        StartCoroutine(SpawnTarget());
        StartCoroutine(SpeedUp());
    }
    /// <summary>
    /// Initializes data on scene startup
    /// </summary>
    private void InitializeData()
    {
        _bestScoreTemplate = _stringsModel.BestScore;
        _currentScoreTemplate = _stringsModel.CurrentScore;
        gameOverObejct.SetActive(false);
        gameOver = false;
        Time.timeScale = 1;

        _currentScore = 0;
        currentScoreText.SetText(string.Format(_currentScoreTemplate, _currentScore));

        bestScoreText.SetText(string.Format(_bestScoreTemplate, PlayerPrefsWrapper.GetKillerBestScore()));
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
    /// Increments score in UI and in class
    /// </summary>
    public void AddScore()
    {
        _currentScore++;
        currentScoreText.SetText(string.Format(_currentScoreTemplate, _currentScore));
    }
    /// <summary>
    /// GameOver sets gameOver flag to true and makes Game Over UI visible
    /// </summary>
    public void GameOver()
    {
        gameOverObejct.SetActive(true);
        gameOver = true;
        DestroyAllTargetsOnScene();

        if (_currentScore > PlayerPrefsWrapper.GetKillerBestScore())
        {
            PlayerPrefsWrapper.SetKillerBestScoreAndSave(_currentScore);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
