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

    private SpawnScenarios spawnScenarios;
    private WallSpawnScenario scenario = new WallSpawnScenario();

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="wallHeightAdditional"></param>
    /// <param name="gameAreaRectTransform"></param>
    public void Initialize(int wallHeightAdditional, RectTransform gameAreaRectTransform)
    {
        float wallBlockWidth = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        wallHeight = (int)(gameAreaRectTransform.rect.height / wallBlockWidth) + wallHeightAdditional;
        spawnScenarios = GetComponent<SpawnScenarios>();
        InitializeWallsPosition(gameAreaRectTransform);
        InitializeWalls();
        RollScenario();
    }
    
    /// <summary>
    /// Restarts WallGenerator.
    /// </summary>
    public void Restart()
    {
        leftWall = Wall.Initialize(leftWallInitialPosition, wallBlockWidth, movableObjectsContainerTransform, DIRECTION_LEFT, normalWallBlockPrefab);
        rightWall = Wall.Initialize(rightWallInitialPosition, wallBlockWidth, movableObjectsContainerTransform, DIRECTION_RIGHT, normalWallBlockPrefab);
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

        Transform movableObjectsContainer = GetComponent<MovableObjectsContainer>().transform;
        leftWall = Wall.Initialize(leftWallInitialPosition, wallBlockWidth, movableObjectsContainer, DIRECTION_LEFT, normalWallBlockPrefab);
        rightWall = Wall.Initialize(rightWallInitialPosition, wallBlockWidth, movableObjectsContainer, DIRECTION_RIGHT, normalWallBlockPrefab);
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

    /// <summary>
    /// Generates new wall blocks on top of walls
    /// </summary>
    /// <param name="blocksPair">WallBlocks prefabs to be initialized</param>
    public void GenerateNewWallsOnTop((BaseWallBlock, BaseWallBlock) blocksPair)
    {
        leftWall.AddWallBlockOnTop(blocksPair.Item1);
        rightWall.AddWallBlockOnTop(blocksPair.Item2);
    }

    /// <summary>
    /// Chooses what blocks to initialize based on current scenario
    /// </summary>
    public void GenerateNextWallFromScenario()
    {
        if(scenario.GetNextBlocks() == (null,null))
        {
            RollScenario();
        }
        var nextBlocksPairEnum = scenario.GetNextBlocks();
        scenario.RemoveLastPairOfBlocks();

        var nextBlocksPair = (EnumToPrefab(nextBlocksPairEnum.Item1), EnumToPrefab(nextBlocksPairEnum.Item2));
        GenerateNewWallsOnTop(nextBlocksPair);
    }

    /// <summary>
    /// Converts enum to blocks prefabs
    /// </summary>
    /// <param name="block">Block enum</param>
    /// <returns>Block prefab</returns>
    private BaseWallBlock EnumToPrefab(WallBlocks? block)
    {
        switch (block)
        {
            case WallBlocks.baseWallBlock:
                return normalWallBlockPrefab;

            case WallBlocks.slimeWallBlock:
                return slimeWallBlockPrefab;

            case WallBlocks.spikeWallBlock:
                return spikeWallBlockPrefab;

            default:
                Debug.Log("Fuck up");
                return normalWallBlockPrefab;
        }
    }

    /// <summary>
    /// Rolls new scenario
    /// </summary>
    private void RollScenario()
    {
        scenario = spawnScenarios.GetRandomScenario();
    }
}
