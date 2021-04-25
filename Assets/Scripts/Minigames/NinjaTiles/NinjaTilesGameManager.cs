using Assets.Scripts.Minigames.NinjaTiles;
using System.Collections.Generic;
using UnityEngine;

public class NinjaTilesGameManager : MonoBehaviour
{
    #region singleton
    private static NinjaTilesGameManager instance = null;

    public static NinjaTilesGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NinjaTilesGameManager>();

                if (instance == null)
                {
                    instance = new NinjaTilesGameManager();
                }
            }
            return instance;
        }
    }
    #endregion

    [Header("Ninja")]
    [SerializeField] private float ninja_position_y;
    [SerializeField] private float ninja_position_z;

    [Header("Main objects")]
    [SerializeField] private GameObject tilesContainerPrefab;
    [SerializeField] private GameObject ninjaPrefab;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject gameObjectsContainer;
    [SerializeField] private GameObject scalableGameObjectsContainer;
    [SerializeField] private GameObject spaceshipPrefab;

    [Header("Game mechanics")]
    public float startSpeed;
    public float startAcceleration;
    public float accelerationLoss;
    public int numberOfSpaceships;
    private float currentScore;
    private float bestScore;
    private bool gameOver;

    private GameObject tilesContainer;
    private Ninja ninja;

    private const float UNCHANGED_SCALE_FACTOR = 1;
    private const int START_TILE_INDEX = 2;

    public static int TileIndexShift
    {
        get;
    } = 2;

    public static Vector2 ScreenScale
    {
        get;
        private set;
    }

    void Start()
    {
        InitializeData();
        StartGame();
    }

    private void StartGame()
    {
        StartSpaceshipAboveTileAtIndex(START_TILE_INDEX);
    }

    /// <summary>
    /// First spaceship on mission to spot the ninja initialized above him in the middle (lucky spaceship)
    /// </summary>
    /// <param name="tileIndex"></param>
    private void StartSpaceshipAboveTileAtIndex(int tileIndex)
    {
        Spaceship spaceship = Instantiate(spaceshipPrefab).GetComponent<Spaceship>();
        spaceship.transform.SetParent(gameObjectsContainer.transform);
        spaceship.MoveSpaceshipAboveTileAtIndex(tileIndex);
    }

    /// <summary>
    /// Initializes data on scene startup
    /// </summary>
    private void InitializeData()
    {
        SetupData();
        SetupTransformForGameObjects();
    }

    private void SetupData()
    {
        tilesContainer = Instantiate(tilesContainerPrefab);
        ninja = Instantiate(ninjaPrefab).GetComponent<Ninja>();
    }

    /// <summary>
    /// Set objects' width on the scene based on the device resolution
    /// </summary>
    private void SetupTransformForGameObjects()
    {
        Transform ninjaTransform = ninja.transform;

        tilesContainer.transform.SetParent(scalableGameObjectsContainer.transform);
        ninjaTransform.SetParent(gameObjectsContainer.transform);

        float widthScaleForTilesContainer = mainCamera.GetComponent<Camera>().orthographicSize * 2 / tilesContainer.GetComponentInChildren<SpriteRenderer>().size.x;
        ScreenScale = new Vector2(widthScaleForTilesContainer * Screen.width / Screen.height, UNCHANGED_SCALE_FACTOR);

        scalableGameObjectsContainer.transform.localScale = new Vector3(ScreenScale.x, ScreenScale.y, UNCHANGED_SCALE_FACTOR);
        ninjaTransform.position = new Vector3(ninjaTransform.position.x, ninja_position_y, ninja_position_z);
    }

    /// <summary>
    /// Move ninja to column
    /// </summary>
    /// <param name="columnTile"></param>
    public void MoveNinjaToTile(GameObject columnTile)
    {
        ninja.MoveNinja(columnTile.transform.position);
    }
}
