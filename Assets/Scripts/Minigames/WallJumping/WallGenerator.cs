using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    [Header("References")]
    public NormalWallBlock normalWallBlockPrefab;
    public Wall leftWall;
    public Wall rightWall;

    [Header("Game attributes")]
    public int wallHeight;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
        GenerateInitialWall();
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

        rt.sizeDelta = screenBounds * 2;
        rt.anchoredPosition = new Vector2(-screenBounds.x, -screenBounds.y);
        //Rect rectContainer = rt.rect;
        //rectContainer.height = screenBounds.y;
        //rectContainer.width = screenBounds.x;
        //rectContainer.position = new Vector2(-screenBounds.x, -screenBounds.y);
        //rt.anchoredPosition = new Vector2(-screenBounds.x, -screenBounds.y);

        float bottomWallYCoord = rt.anchoredPosition.y + wallBlockHeight / 2;
        Vector2 leftWallInitialPosition = new Vector2(rt.anchoredPosition.x + wallBlockWidth / 2, bottomWallYCoord);
        Vector2 rightWallInitialPosition = new Vector2(rt.anchoredPosition.x + rt.sizeDelta.x - wallBlockWidth / 2, bottomWallYCoord);

        //float bottomWallYCoord = -rt.sizeDelta.y + wallBlockHeight / 2;
        //Vector2 leftWallInitialPosition = new Vector2(-rt.sizeDelta.x + wallBlockWidth / 2, bottomWallYCoord);
        //Vector2 rightWallInitialPosition = new Vector2(rt.sizeDelta.x - wallBlockWidth / 2, bottomWallYCoord);

        leftWall.Initialize(leftWallInitialPosition, wallBlockHeight);
        rightWall.Initialize(rightWallInitialPosition, wallBlockHeight);
    }

    /// <summary>
    /// Generates initial wall.
    /// </summary>
    private void GenerateInitialWall()
    {
        for (int _ = 0; _ < wallHeight; _++)
        {
            leftWall.AddWallBlockOnTop(normalWallBlockPrefab);
            rightWall.AddWallBlockOnTop(normalWallBlockPrefab);
        }
    }
}
