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
    [SerializeField] private GameObject spaceshipPrefab;

    private GameObject tilesContainer;
    private Ninja ninja;
    private List<Spaceship> spaceshipList;
    private Vector2 screenScale;

    private const float UNCHANGED_SCALE_FACTOR = 1;

    void Start()
    {
        InitializeData();
        StartGame();
    }

    private void StartGame()
    {
        StartSpaceshipAboveTileAtIndex(0);
    }

    private void StartSpaceshipAboveTileAtIndex(int tileIndex)
    {
        Spaceship spaceship = Instantiate(spaceshipPrefab).GetComponent<Spaceship>();
        spaceship.transform.position = new Vector2(tileIndex - 2, 0);
        spaceship.transform.SetParent(gameObjectsContainer.transform);
    }

    /// <summary>
    /// Initializes data on scene startup
    /// </summary>
    private void InitializeData()
    {
        SetData();
        SetTransformForGameObjects();
    }

    private void SetData()
    {
        tilesContainer = Instantiate(tilesContainerPrefab);
        ninja = Instantiate(ninjaPrefab).GetComponent<Ninja>();
    }

    /// <summary>
    /// Set objects' width on the scene based on the device resolution
    /// </summary>
    private void SetTransformForGameObjects()
    {
        tilesContainer.transform.SetParent(gameObjectsContainer.transform);
        ninja.transform.parent = gameObjectsContainer.transform;

        float widthScaleForTilesContainer = mainCamera.GetComponent<Camera>().orthographicSize * 2 / tilesContainer.GetComponentInChildren<SpriteRenderer>().size.x;
        screenScale = new Vector2(widthScaleForTilesContainer * Screen.width / Screen.height, UNCHANGED_SCALE_FACTOR);
        
        gameObjectsContainer.transform.localScale = new Vector3(screenScale.x, screenScale.y, UNCHANGED_SCALE_FACTOR);
        ninja.transform.position = new Vector3(ninja.transform.position.x, ninja_position_y + (screenScale.x - ninja.transform.localScale.y) * ninja.GetComponent<SpriteRenderer>().bounds.size.y / 2, ninja_position_z);
        ninja.transform.localScale = new Vector3(UNCHANGED_SCALE_FACTOR, screenScale.x, UNCHANGED_SCALE_FACTOR);
    }

    /// <summary>
    /// Move ninja to column
    /// </summary>
    /// <param name="columnTile"></param>
    public void MoveNinjaToTile(GameObject columnTile)
    {
        ninja.GetComponent<Ninja>().MoveNinja(columnTile.transform.position);
    }
}
