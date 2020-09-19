using UnityEngine;

public class GameArea : MonoBehaviour
{
    private RectTransform gameAreaRectTransform;
    private WallGenerator wallGenerator;
    private MovableObjectsContainer movableObjectsContainer;
    private WallJumpingPlayer player;
    private BackgroundGenerator backgroundGenerator;

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="heightRatio"></param>
    public void Initialize(float heightRatio, int wallHeightAdditional, float garbageCollectorHeight)
    {
        gameAreaRectTransform = GetComponent<RectTransform>();
        player = GetComponentInChildren<WallJumpingPlayer>();
        movableObjectsContainer = GetComponentInChildren<MovableObjectsContainer>();
        backgroundGenerator = GetComponentInChildren<BackgroundGenerator>();
        InitializePosition(heightRatio);
        InitializeMovableObjectsContainer();
        InitializePlayerPosition();
        InitializeWallGenerator(wallHeightAdditional);
        InitializeGarbageCollectorPosition(garbageCollectorHeight);
        InitializeBackgroundGenerator(wallHeightAdditional);
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
    /// Initializes Player position.
    /// </summary>
    private void InitializePlayerPosition()
    {
        float playerInitialYCoord = 3; // this will be removed when floor is added (it is only for testing)
        Vector2 playerInitialPosition = new Vector2(gameAreaRectTransform.sizeDelta.x / 2, playerInitialYCoord);
        player.InitializePosition(playerInitialPosition);
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
    /// <param name="wallHeightAdditional"></param>
    private void InitializeBackgroundGenerator(int wallHeightAdditional)
    {
        backgroundGenerator.Initialize(gameAreaRectTransform, wallHeightAdditional);
    }
}
