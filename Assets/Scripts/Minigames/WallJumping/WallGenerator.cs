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

    public void Initialize(float heightRatio)
    {
        InitializePosition(heightRatio);
        InitializeWalls();
    }

    private void InitializePosition(float heightRatio)
    {
        gameAreaRectTransform = GetComponent<RectTransform>();
        wallBlockWidth = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float screenBottomPosition = -screenBounds.y;

        screenBounds = new Vector2(screenBounds.x, screenBounds.y * heightRatio);
        gameAreaRectTransform.sizeDelta = screenBounds * 2;
        gameAreaRectTransform.anchoredPosition = new Vector2(-screenBounds.x, screenBottomPosition);        
    }

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
