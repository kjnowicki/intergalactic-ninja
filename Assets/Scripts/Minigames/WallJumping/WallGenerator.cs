using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    [Header("References")]
    public NormalWallBlock normalWallBlockPrefab;
    public Wall leftWall;
    public Wall rightWall;
    //public RectTransform rectTransform;
    //public RectTransform parentRectTransform;

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
        //rectTransform = GetComponent<RectTransform>();
        ////parentRectTransform = transform.parent.GetComponent<RectTransform>();
        //RectTransform UIRectTransform = parentRectTransform.Find("UI").GetComponent<RectTransform>();
        //parentRectTransform.localScale = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        //screenBounds = rectTransform.sizeDelta;
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        float wallBlockWidth = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float wallBlockHeight = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        rectTransform.sizeDelta = screenBounds * 2;
        rectTransform.anchoredPosition = new Vector2(-screenBounds.x, -screenBounds.y);
        float bottomWallYCoord = -screenBounds.y + wallBlockHeight / 2;
        Vector2 leftWallInitialPosition = new Vector2(-screenBounds.x + wallBlockWidth / 2, bottomWallYCoord);
        Vector2 rightWallInitialPosition = new Vector2(screenBounds.x - wallBlockWidth / 2, bottomWallYCoord);

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
