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

    private GameObject tilesContainer;
    private Ninja ninja;
    private Vector2 screenScale;

    private const float UNCHANGED_SCALE_FACTOR = 1;
    private const int START_TILE_INDEX = 2;
    private const int TILE_INDEX_SHIFT = 2;

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
        Transform spaceshipTransform = spaceship.transform;
        spaceshipTransform.SetParent(gameObjectsContainer.transform);
        spaceshipTransform.position = new Vector2((tileIndex - TILE_INDEX_SHIFT) * screenScale.x, 0);
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
        screenScale = new Vector2(widthScaleForTilesContainer * Screen.width / Screen.height, UNCHANGED_SCALE_FACTOR);

        scalableGameObjectsContainer.transform.localScale = new Vector3(screenScale.x, screenScale.y, UNCHANGED_SCALE_FACTOR);
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
