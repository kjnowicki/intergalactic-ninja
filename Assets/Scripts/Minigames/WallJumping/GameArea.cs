using System;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    private RectTransform gameAreaRectTransform;
    private WallGenerator wallGenerator;
    private MovableObjectsContainer movableObjectsContainer;
    private WallJumpingPlayer player;
    private BackgroundGenerator backgroundGenerator;
    private CameraRubberBand cameraRubberBand;

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="wallJumpingGameManager"></param>
    /// <param name="heightRatio"></param>
    /// <param name="wallHeightAdditional"></param>
    /// <param name="garbageCollectorHeight"></param>
    /// <param name="slimeEffectDuration"></param>
    public void Initialize(WallJumpingGameManager wallJumpingGameManager, float heightRatio, int wallHeightAdditional, float garbageCollectorHeight, float slimeEffectDuration)
    {
        gameAreaRectTransform = GetComponent<RectTransform>();
        movableObjectsContainer = GetComponentInChildren<MovableObjectsContainer>();
        cameraRubberBand = FindObjectOfType<CameraRubberBand>();
        backgroundGenerator = GetComponentInChildren<BackgroundGenerator>();

        InitializePosition(heightRatio);
        InitializeMovableObjectsContainer();
        InitializeWallGenerator(wallHeightAdditional);
        InitializeGarbageCollectorPosition(garbageCollectorHeight);
        InitializeBackgroundGenerator();
        InitializeSlimeGenerator(slimeEffectDuration);
        InitializeRubberBandAreaSize(wallJumpingGameManager);
    }

    /// <summary>
    /// Initializes Player.
    /// </summary>
    public void InitializePlayer(float playerForceScale, WallJumpingGameManager wallJumpingGameManager)
    {
        movableObjectsContainer.InitializePlayer(playerForceScale, wallJumpingGameManager);
    }

    /// <summary>
    /// Initializes GameArea position.
    /// </summary>
    /// <param name="heightRatio"></param>
    private void InitializePosition(float heightRatio)
    {
        
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 gameAreaBounds = new Vector2(screenBounds.x, screenBounds.y * heightRatio);
        gameAreaRectTransform.sizeDelta = gameAreaBounds * 2;
        gameAreaRectTransform.anchoredPosition = new Vector2(-gameAreaBounds.x, -screenBounds.y);
    }

    /// <summary>
    /// Initializes MovableObjectsContainer.
    /// </summary>
    private void InitializeMovableObjectsContainer()
    {
        movableObjectsContainer.Initialize(gameAreaRectTransform);
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
    /// Initializes GarbageCollector position.
    /// </summary>
    /// <param name="garbageCollectorHeight"></param>
    private void InitializeGarbageCollectorPosition(float garbageCollectorHeight)
    {
        GarbageCollector garbageCollector = GetComponentInChildren<GarbageCollector>();
        Vector2 garbageCollectorInitialPosition = new Vector2(gameAreaRectTransform.sizeDelta.x / 2, garbageCollectorHeight);
        garbageCollector.InitializePosition(garbageCollectorInitialPosition);
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
        SlimeGenerator slimeGenerator = FindObjectOfType<SlimeGenerator>();
        slimeGenerator.Initialize(gameAreaRectTransform, slimeEffectDuration);
    }

    /// <summary>
    /// Initializes size of the trigger that is responsible for rubber banding camera
    /// </summary>
    /// <param name="wallJumpingGameManager"></param>
    private void InitializeRubberBandAreaSize(WallJumpingGameManager wallJumpingGameManager)
    {
        Vector2 restrictedAreaPosition = gameAreaRectTransform.position;
        Vector2 restrictedAreaSize = gameAreaRectTransform.sizeDelta;
        cameraRubberBand.Initialize(wallJumpingGameManager, restrictedAreaPosition, restrictedAreaSize);
    }
}
