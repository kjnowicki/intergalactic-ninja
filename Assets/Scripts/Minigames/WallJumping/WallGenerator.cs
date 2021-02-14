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
    private float wallBlockWidth;
    private Transform movableObjectsContainerTransform;
    private Vector2 leftWallInitialPosition;
    private Vector2 rightWallInitialPosition;

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
    
    public void Restart()
    {
        leftWall = new Wall(leftWallInitialPosition, wallBlockWidth, movableObjectsContainerTransform, DIRECTION_LEFT);
        rightWall = new Wall(rightWallInitialPosition, wallBlockWidth, movableObjectsContainerTransform, DIRECTION_RIGHT);
        GenerateInitialWall(wallHeight);
    }

    /// <summary>
    /// Initializes Walls position.
    /// </summary>
    /// <param name="gameAreaRectTransform"></param>
    private void InitializeWallsPosition(RectTransform gameAreaRectTransform)
    {
        wallBlockWidth = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float bottomWallYCoord = gameAreaRectTransform.anchoredPosition.y + wallBlockWidth / 2;
        leftWallInitialPosition = new Vector2(gameAreaRectTransform.anchoredPosition.x + wallBlockWidth / 2, bottomWallYCoord);
        rightWallInitialPosition = new Vector2(gameAreaRectTransform.anchoredPosition.x + gameAreaRectTransform.rect.width - wallBlockWidth / 2, bottomWallYCoord);

        movableObjectsContainerTransform = GetComponent<MovableObjectsContainer>().transform;
        leftWall = new Wall(leftWallInitialPosition, wallBlockWidth, movableObjectsContainerTransform, DIRECTION_LEFT);
        rightWall = new Wall(rightWallInitialPosition, wallBlockWidth, movableObjectsContainerTransform, DIRECTION_RIGHT);
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
        for (int _ = 0; _ < wallHeight; _++)
        {
            leftWall.AddWallBlockOnTop(normalWallBlockPrefab);
            rightWall.AddWallBlockOnTop(normalWallBlockPrefab);
        }
    }
}
