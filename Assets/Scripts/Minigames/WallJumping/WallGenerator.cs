using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    private const int DIRECTION_LEFT = 1;
    private const int DIRECTION_RIGHT = -1;

    private Wall leftWall;
    private Wall rightWall;
    private int wallHeight; // in number of blocks

    [Header("References")]
    public NormalWallBlock normalWallBlockPrefab;
    public SpikeWallBlock spikeWallBlockPrefab;
    public SlimeWallBlock slimeWallBlockPrefab;

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="wallHeightAdditional"></param>
    /// <param name="gameAreaRectTransform"></param>
    public void Initialize(int wallHeightAdditional, RectTransform gameAreaRectTransform)
    {
        float wallBlockWidth = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        wallHeight = (int)(gameAreaRectTransform.rect.height / wallBlockWidth) + wallHeightAdditional;
        InitializeWallsPosition(gameAreaRectTransform);
        InitializeWalls();
    }

    /// <summary>
    /// Initializes Walls position.
    /// </summary>
    /// <param name="gameAreaRectTransform"></param>
    private void InitializeWallsPosition(RectTransform gameAreaRectTransform)
    {
        var wallBlockWidth = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float bottomWallYCoord = gameAreaRectTransform.anchoredPosition.y + wallBlockWidth / 2;
        Vector2 leftWallInitialPosition = new Vector2(gameAreaRectTransform.anchoredPosition.x + wallBlockWidth / 2, bottomWallYCoord);
        Vector2 rightWallInitialPosition = new Vector2(gameAreaRectTransform.anchoredPosition.x + gameAreaRectTransform.rect.width - wallBlockWidth / 2, bottomWallYCoord);

        Transform movableObjectsContainer = GetComponent<MovableObjectsContainer>().transform;
        leftWall = new Wall(leftWallInitialPosition, wallBlockWidth, movableObjectsContainer, DIRECTION_LEFT);
        rightWall = new Wall(rightWallInitialPosition, wallBlockWidth, movableObjectsContainer, DIRECTION_RIGHT);
    }

    /// <summary>
    /// Initializes Walls.
    /// </summary>
    private void InitializeWalls()
    {
        GenerateInitialWall(wallHeight);
    }

    /// <summary>
    /// Generates initial wall.
    /// </summary>
    /// <param name="wallHeight"></param>
    private void GenerateInitialWall(int wallHeight)
    {
        // Just for testing 
        // --------------------------
        for (int _ = 0; _ < wallHeight; _++)
        {
            leftWall.AddWallBlockOnTop(spikeWallBlockPrefab);
            rightWall.AddWallBlockOnTop(slimeWallBlockPrefab);
        }
        // --------------------------

        //for (int _ = 0; _ < wallHeight; _++)
        //{
        //    leftWall.AddWallBlockOnTop(normalWallBlockPrefab);
        //    rightWall.AddWallBlockOnTop(normalWallBlockPrefab);
        //}
    }
}
