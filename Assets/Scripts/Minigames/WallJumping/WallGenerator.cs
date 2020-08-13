using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    [Header("References")]
    public NormalWallBlock normalWallBlockPrefab;
    public Wall leftWall;
    public Wall rightWall;

    [Header("Game attributes")]
    public int wallHeightAdditional;

    public void Initialize(float heightRatio)
    {
        RectTransform rt = GetComponent<RectTransform>();
        float wallBlockWidth = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float wallBlockHeight = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float screenBottomPosition = -screenBounds.y;

        screenBounds = new Vector2(screenBounds.x, screenBounds.y *  2);
        rt.sizeDelta = screenBounds * 2;
        rt.anchoredPosition = new Vector2(-screenBounds.x, screenBottomPosition);

        float bottomWallYCoord = rt.anchoredPosition.y + wallBlockHeight / 2;
        Vector2 leftWallInitialPosition = new Vector2(rt.anchoredPosition.x + wallBlockWidth / 2, bottomWallYCoord);
        Vector2 rightWallInitialPosition = new Vector2(rt.anchoredPosition.x + rt.rect.width - wallBlockWidth / 2, bottomWallYCoord);

        int wallHeight = (int)(rt.rect.height / wallBlockHeight) + wallHeightAdditional;

        leftWall.Initialize(leftWallInitialPosition, wallBlockHeight);
        rightWall.Initialize(rightWallInitialPosition, wallBlockHeight);

        GenerateInitialWall(wallHeight);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
        //GenerateInitialWall();
    }

    /// <summary>
    /// Calculates the screen coordinates and initialize the walls.
    /// </summary>
    private void SetUp()
    {
        RectTransform rt = GetComponent<RectTransform>();
        float wallBlockWidth = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float wallBlockHeight = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float screenBottomPosition = -screenBounds.y;

        screenBounds = new Vector2(screenBounds.x, screenBounds.y / 2);
        rt.sizeDelta = screenBounds * 2;
        rt.anchoredPosition = new Vector2(-screenBounds.x, screenBottomPosition);

        float bottomWallYCoord = rt.anchoredPosition.y + wallBlockHeight / 2;
        Vector2 leftWallInitialPosition = new Vector2(rt.anchoredPosition.x + wallBlockWidth / 2, bottomWallYCoord);
        Vector2 rightWallInitialPosition = new Vector2(rt.anchoredPosition.x + rt.rect.width - wallBlockWidth / 2, bottomWallYCoord);

        int wallHeight = (int)(rt.rect.height / wallBlockHeight) + wallHeightAdditional;

        leftWall.Initialize(leftWallInitialPosition, wallBlockHeight);
        rightWall.Initialize(rightWallInitialPosition, wallBlockHeight);

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
