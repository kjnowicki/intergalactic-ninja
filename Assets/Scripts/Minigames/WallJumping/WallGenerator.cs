using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    [Header("References")]
    public NormalWallBlock normalWallBlockPrefab;
    public Wall leftWall;
    public Wall rightWall;

    [Header("Game attributes")]
    public int wallHeightAdditional;

    private RectTransform gameAreaRectTransform;
    private float wallBlockWidth;
    private int wallHeight; // in number of blocks

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="heightRatio"></param>
    public void Initialize(float heightRatio)
    {
        InitializePosition(heightRatio);
        InitializeWalls();
        InitializePlayer();
    }

    /// <summary>
    /// Initializes GameArea position.
    /// </summary>
    /// <param name="heightRatio"></param>
    private void InitializePosition(float heightRatio)
    {
        gameAreaRectTransform = GetComponent<RectTransform>();
        wallBlockWidth = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Vector2 gameAreaBounds = new Vector2(screenBounds.x, screenBounds.y * heightRatio);
        gameAreaRectTransform.sizeDelta = gameAreaBounds * 2;
        gameAreaRectTransform.anchoredPosition = new Vector2(-gameAreaBounds.x, -screenBounds.y);
    }

    /// <summary>
    /// Initializes Walls.
    /// </summary>
    private void InitializeWalls()
    {
        float bottomWallYCoord = gameAreaRectTransform.anchoredPosition.y + wallBlockWidth / 2;
        Vector2 leftWallInitialPosition = new Vector2(gameAreaRectTransform.anchoredPosition.x + wallBlockWidth / 2, bottomWallYCoord);
        Vector2 rightWallInitialPosition = new Vector2(gameAreaRectTransform.anchoredPosition.x + gameAreaRectTransform.rect.width - wallBlockWidth / 2, bottomWallYCoord);

        leftWall.Initialize(leftWallInitialPosition, wallBlockWidth);
        rightWall.Initialize(rightWallInitialPosition, wallBlockWidth);

        wallHeight = (int)(gameAreaRectTransform.rect.height / wallBlockWidth) + wallHeightAdditional;
        GenerateInitialWall(wallHeight);
    }

    /// <summary>
    /// Initializes Player.
    /// </summary>
    private void InitializePlayer()
    {
        float playerInitialYCoord = 1;
        Player player = GetComponentInChildren<Player>();
        Vector2 playerInitialPosition = new Vector2(gameAreaRectTransform.sizeDelta.x / 2, playerInitialYCoord);
        player.Initialize(playerInitialPosition);
    }

    /// <summary>
    /// Generates initial wall.
    /// </summary>
    private void GenerateInitialWall(int wallHeight)
    {
        for (int _ = 0; _ < wallHeight; _++)
        {
            leftWall.AddWallBlockOnTop(normalWallBlockPrefab);
            rightWall.AddWallBlockOnTop(normalWallBlockPrefab);
        }
    }
}
