using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    private RectTransform gameAreaRectTransform;
    private WallGenerator wallGenerator;
    private MovableObjectsContainer movableObjectsContainer;
    private BackgroundGenerator backgroundGenerator;
    private GameObject floorGameObject;

    [SerializeField] private CameraRubberBand cameraRubberBand;
    [SerializeField] private GameObject floorPrefab;

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="wallHeightAdditional"></param>
    /// <param name="garbageCollectorHeight"></param>
    /// <param name="slimeEffectDuration"></param>
    public void Initialize(int wallHeightAdditional, float garbageCollectorHeight, float slimeEffectDuration)
    {
        gameAreaRectTransform = GetComponent<RectTransform>();
        movableObjectsContainer = GetComponentInChildren<MovableObjectsContainer>();
        backgroundGenerator = GetComponentInChildren<BackgroundGenerator>();

        InitializePosition();
        InitializeWallGenerator(wallHeightAdditional);
        InitializeBackgroundGenerator();
        InitializeSlimeGenerator(slimeEffectDuration);
        InitializeRubberBandAreaSize();
        InitializeFloor();
        InitializeMovableObjectsContainer();
    }

    /// <summary>
    /// Initializes Player.
    /// </summary>
    public void InitializePlayer(float playerForceScale)
    {
        movableObjectsContainer.InitializePlayer(playerForceScale);
    }

    /// <summary>
    /// Destroys objects from previous game.
    /// </summary>
    public void DestroyObjectsFromPreviousGame()
    {
        List<Transform> children = new List<Transform>(movableObjectsContainer.GetComponentsInChildren<Transform>());
        children.Remove(movableObjectsContainer.transform);
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Restarts GameArea part of the game.
    /// </summary>
    public void Restart()
    {
        backgroundGenerator.Restart();
        wallGenerator.Restart();
        InitializeFloor();
        movableObjectsContainer.Initialize(floorGameObject);
    }

    /// <summary>
    /// Initializes GameArea position.
    /// </summary>
    private void InitializePosition()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 gameAreaBounds = new Vector2(screenBounds.x * 2, gameAreaRectTransform.sizeDelta.y);
        gameAreaRectTransform.sizeDelta = gameAreaBounds;
        gameAreaRectTransform.localPosition = new Vector2(-screenBounds.x, gameAreaRectTransform.localPosition.y);;
    }

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="wallHeightAdditional"></param>
    private void InitializeWallGenerator(int wallHeightAdditional)
    {
        wallGenerator = GetComponentInChildren<WallGenerator>();
        wallGenerator.Initialize(wallHeightAdditional, gameAreaRectTransform);
    }

    /// <summary>
    /// Initializes BackgroundGenerator.
    /// </summary>
    private void InitializeBackgroundGenerator()
    {
        backgroundGenerator.Initialize(gameAreaRectTransform);
    }

    /// <summary>
    /// Initializes SlimeGenerator.
    /// </summary>
    /// <param name="slimeEffectDuration"></param>
    private void InitializeSlimeGenerator(float slimeEffectDuration)
    {
        SlimeGenerator slimeGenerator = FindAnyObjectByType<SlimeGenerator>();
        slimeGenerator.Initialize(gameAreaRectTransform, slimeEffectDuration);
    }

    /// <summary>
    /// Initializes size of the trigger that is responsible for rubber banding camera
    /// </summary>
    private void InitializeRubberBandAreaSize()
    {
        Vector2 restrictedAreaPosition = gameAreaRectTransform.position;
        Vector2 restrictedAreaSize = gameAreaRectTransform.sizeDelta;
        cameraRubberBand.Initialize(restrictedAreaPosition, restrictedAreaSize);
    }

    private void InitializeFloor()
    {
        floorGameObject = Instantiate(floorPrefab, movableObjectsContainer.transform);
    }
    
    private void InitializeMovableObjectsContainer()
    {
        movableObjectsContainer.Initialize(floorGameObject);
    }
}
