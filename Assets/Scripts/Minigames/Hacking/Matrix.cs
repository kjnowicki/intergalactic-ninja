using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using static UnityEngine.Mathf;
using static UnityEngine.Random;
using System;
using System.Threading.Tasks;
using System.Threading;

public class Matrix : MonoBehaviour
{
    private const string BACKGROUND_TILE_NAME = "BackgroundTile ({0}, {1})";

    private const string BUBBLE_NAME = "Bubble ({0}, {1})";

    private const string BUBBLES_ASSETS_PATH = "Minigames/Hacking/Prefabs/Bubbles";

    [Header("Matrix parameters")]
    public int width;
    public int height;
    public GameObject backgroundTilePrefab;

    private GameObject[,] matrixBackgroundTiles;

    private GameObject[] bubblesPool; // it contains pool of unique bubbles (matrix is filled with those bubbles)

    private DeadlockChecker deadlockChecker;

    private List<Vector2> emptyPositionsBufferList = new List<Vector2>();

    private List<Bubble> recentlyMovedBubblesBufferList = new List<Bubble>();

    private int maxNumberOfMatchingNeighboursInARowOnSetup = 2;

    private Vector2 direction = Vector2.down;
    public Vector2 Direction
    {
        get => direction;
        set
        {
            direction = value;
            GameManagerHacking.Instance.RotateGravityArrow(direction);
        }
    }

    private int comboMultiplier = 1;

    public GameObject[,] MatrixBubbles { get; set; }

    void Awake()
    {
        matrixBackgroundTiles = new GameObject[width, height];
        MatrixBubbles = new GameObject[width, height];
        bubblesPool = Resources.LoadAll<GameObject>(BUBBLES_ASSETS_PATH);
        deadlockChecker = new DeadlockChecker(this);
        SetUp();
        BubbleAnimation.RandomIdleAnimation(this);
    }

    /// <summary>
    /// Method is setting all Matrix with BackgroundTiles (unmovable) and bubbles (movable)
    /// </summary>
    private void SetUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tilePosition = new Vector2(i, j);
                SetBackgroundTileAtPosition(tilePosition);
                SetBubbleAtPosition(tilePosition);
            }
        }
        CheckMatrixDeadlock();
    }

    /// <summary>
    /// Checks if matrix is on deadlock. If it is the matrix gets resetted
    /// </summary>
    private void CheckMatrixDeadlock()
    {
        if (deadlockChecker.IsMatrixOnDeadlock())
        {
            Debug.Log("Matrix on deadlock. Resetting...");
            Reset();
        }
        Direction = Vector2.Perpendicular(Direction);
        GameManagerHacking.Instance.EnableControl();
    }

    /// <summary>
    /// Instantiates a background tile at position
    /// </summary>
    /// <param name="tilePosition"></param>
    private void SetBackgroundTileAtPosition(Vector2 tilePosition)
    {
        GameObject backgroundTile = Instantiate(backgroundTilePrefab, tilePosition, Quaternion.identity, transform);
        backgroundTile.name = string.Format(BACKGROUND_TILE_NAME, (int)tilePosition.x, (int)tilePosition.y);
        matrixBackgroundTiles[(int)tilePosition.x, (int)tilePosition.y] = backgroundTile;
    }

    /// <summary>
    /// Instantiates a bubble at given position
    /// </summary>
    /// <param name="tilePosition"></param>
    private Bubble SetBubbleAtPosition(Vector2 tilePosition)
    {
        IEnumerable<GameObject> availableBubblesPool = bubblesPool;
        if (tilePosition.x >= maxNumberOfMatchingNeighboursInARowOnSetup) availableBubblesPool = availableBubblesPool.Where(bubbleObject => bubbleObject.GetComponent<Bubble>().color != MatrixBubbles[(int)tilePosition.x - maxNumberOfMatchingNeighboursInARowOnSetup, (int)tilePosition.y].GetComponent<Bubble>().color);
        if (tilePosition.y >= maxNumberOfMatchingNeighboursInARowOnSetup) availableBubblesPool = availableBubblesPool.Where(bubbleObject => bubbleObject.GetComponent<Bubble>().color != MatrixBubbles[(int)tilePosition.x, (int)tilePosition.y - maxNumberOfMatchingNeighboursInARowOnSetup].GetComponent<Bubble>().color);
        int bubbleToUseIndex = Range(0, availableBubblesPool.Count());
        GameObject bubble = Instantiate(availableBubblesPool.ElementAt(bubbleToUseIndex), tilePosition, Quaternion.identity, transform);
        bubble.name = string.Format(BUBBLE_NAME, (int)tilePosition.x, (int)tilePosition.y);
        MatrixBubbles[(int)tilePosition.x, (int)tilePosition.y] = bubble;
        return bubble.GetComponent<Bubble>();
    }

    /// <summary>
    /// Instantiates a bubble at given position outside the matrix
    /// </summary>
    /// <param name="tilePosition"></param>
    private Bubble SetBubbleAtOutsidePosition(Vector2 tilePosition)
    {
        int bubbleToUseIndex = Range(0, bubblesPool.Length);
        GameObject bubble = Instantiate(bubblesPool[bubbleToUseIndex], tilePosition, Quaternion.identity, transform);
        bubble.name = string.Format(BUBBLE_NAME, (int)tilePosition.x, (int)tilePosition.y);
        bubble.SetActive(false);
        return bubble.GetComponent<Bubble>();
    }

    /// <summary>
    /// Resets the matrix and setups it from scratch
    /// </summary>
    public void Reset()
    {
        foreach (GameObject bubble in MatrixBubbles) Destroy(bubble);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tilePosition = new Vector2(i, j);
                SetBubbleAtPosition(tilePosition);
            }
        }
        CheckMatrixDeadlock();
    }

    /// <summary>
    /// Swaps bubbles and checks if there is a match.
    /// If there is no match the bubbles are swapped back,
    /// if there is a match the matched bubbles are removed and control is enabled.
    /// </summary>
    /// <param name="bubble1"></param>
    /// <param name="bubble2"></param>
    public void SwapBubbles(Bubble bubble1, Bubble bubble2)
    {
        Task swappingTask = new Task(async () =>
         {
             GameManagerHacking.Instance.DisableControl();
             await BubbleMover.SwapBubbles(bubble1, bubble2);
             MatrixBubbles[bubble1.column, bubble1.row] = bubble1.gameObject;
             MatrixBubbles[bubble2.column, bubble2.row] = bubble2.gameObject;

             int numberOfMatchedBubble1 = MatchFinder.RemoveBubbleAndMatchesIfExist(bubble1, ref comboMultiplier);
             int numberOfMatchedBubble2 = 0;

             bool isBubble1Matched = numberOfMatchedBubble1 != 0;
             bool isBubble2Matched = false;
             if (!emptyPositionsBufferList.Contains(bubble2.transform.position))
             {
                 numberOfMatchedBubble2 = MatchFinder.RemoveBubbleAndMatchesIfExist(bubble2, ref comboMultiplier);
                 isBubble2Matched = numberOfMatchedBubble2 != 0;
             }
             if (!isBubble1Matched && !isBubble2Matched)
             {
                 await BubbleMover.SwapBubbles(bubble1, bubble2);
                 MatrixBubbles[bubble1.column, bubble1.row] = bubble1.gameObject;
                 MatrixBubbles[bubble2.column, bubble2.row] = bubble2.gameObject;
                 GameManagerHacking.Instance.EnableControl();
                 return;
             }
             FillEmptySpace();
         });
        swappingTask.RunSynchronously();
    }

    /// <summary>
    /// Moves babbles
    /// </summary>
    /// <param name="bubble"></param>
    /// <param name="newPosition"></param>
    /// <param name="moveTimeMultiplier"></param>
    /// <returns></returns>
    public async Task MoveBubble(Bubble bubble, Vector2 newPosition, int moveTimeMultiplier)
    {
        await BubbleMover.MoveBubble(bubble, newPosition, moveTimeMultiplier);
        MatrixBubbles[(int)newPosition.x, (int)newPosition.y] = bubble.gameObject;
        recentlyMovedBubblesBufferList.Add(bubble);
    }

    /// <summary>
    /// Adds empty position to matrix bufffer of empty positions
    /// </summary>
    /// <param name="column"></param>
    /// <param name="row"></param>
    public void AddEmptyPositionToBuffer(int column, int row)
    {
        emptyPositionsBufferList.Add(new Vector2(column, row));
    }

    /// <summary>
    /// Fill empty spaces gathered in matrix's empty positions buffer
    /// </summary>
    public void FillEmptySpace()
    {
        Task adjustingTask = new Task(async () =>
        {
            List<Task> tasks = new List<Task>();
            tasks.AddRange(AdjustEmptySpace(emptyPositionsBufferList, Direction));
            tasks.AddRange(FillEmptySpace(emptyPositionsBufferList, Direction));
            await Task.WhenAll(tasks);

            emptyPositionsBufferList.Clear();
            CheckMatchesForRecentlyMovedBubbles();
        });
        adjustingTask.RunSynchronously();
    }

    /// <summary>
    /// Tasking the creation and moving of new bubbles into empty positions in specified direction
    /// </summary>
    /// <param name="emptyPositionsBufferList"></param>
    /// <param name="direction"></param>
    /// <returns>List of tasks to move new bubbles into empty positions</returns>
    private List<Task> FillEmptySpace(List<Vector2> emptyPositionsBufferList, Vector2 direction)
    {
        int yFlag = (int)Abs(direction.y);
        int xFlag = (int)Abs(direction.x);
        List<Task> tasks = new List<Task>();
        foreach (Vector2 position in emptyPositionsBufferList)
        {
            int emptySpacesInColumnCount = emptyPositionsBufferList.Where(vector => vector.x * yFlag + vector.y * xFlag == position.x * yFlag + position.y * xFlag).Count();
            Bubble bubble = SetBubbleAtOutsidePosition(new Vector2(position.x - direction.x * emptySpacesInColumnCount, position.y - direction.y * emptySpacesInColumnCount));
            tasks.Add(MoveBubble(bubble, position, emptySpacesInColumnCount));
        }
        return tasks;
    }

    /// <summary>
    /// Tasking the moving of existing bubbles into empty positions in specified direction
    /// </summary>
    /// <param name="emptyPositions"></param>
    /// <param name="direction"></param>
    /// <returns>List of tasks to move existing bubbles into empty positions</returns>
    private List<Task> AdjustEmptySpace(List<Vector2> emptyPositions, Vector2 direction)
    {
        int yFlag = (int)Abs(direction.y);
        int xFlag = (int)Abs(direction.x);
        List<Task> tasks = new List<Task>();
        List<int> dimensionIndexerList = emptyPositions.Select(vector => (int)vector.x * yFlag + (int)vector.y * xFlag).Distinct().ToList();

        foreach (int dimensionIndex in dimensionIndexerList)
        {
            int emptySpacesInIndexerCount = emptyPositions.Where(vector => (int)vector.x * yFlag + (int)vector.y * xFlag == dimensionIndex).Count();
            Vector2 adjustedDirection = new Vector2(direction.x * xFlag * emptySpacesInIndexerCount, direction.y * yFlag * emptySpacesInIndexerCount);

            for (int secondDimensionIndex = 0; secondDimensionIndex < height * yFlag + width * xFlag; secondDimensionIndex++)
            {
                int row = yFlag != 0 ? (direction.y > 0 ? height - 1 - secondDimensionIndex : secondDimensionIndex) : dimensionIndex;
                int column = xFlag != 0 ? (direction.x > 0 ? width - 1 - secondDimensionIndex : secondDimensionIndex) : dimensionIndex;

                if (MatrixBubbles[column, row] != null)
                {
                    Vector2 newPosition = new Vector2(column + adjustedDirection.x, row + adjustedDirection.y);

                    if (emptyPositions.Contains(newPosition))
                    {
                        Bubble bubble = MatrixBubbles[column, row].GetComponent<Bubble>();

                        tasks.Add(MoveBubble(bubble, newPosition, emptySpacesInIndexerCount));
                        MatrixBubbles[column, row] = null;
                        emptyPositions.Add(new Vector2(column, row));
                        emptyPositions.Remove(newPosition);
                    }
                }
            }
        }
        emptyPositionsBufferList = emptyPositions;
        return tasks;
    }

    /// <summary>
    /// Checks if any of the recent moves in matrix caused a match
    /// </summary>
    private void CheckMatchesForRecentlyMovedBubbles()
    {
        List<Bubble> unmatchedBubbles = new List<Bubble>();
        foreach (Bubble bubble in recentlyMovedBubblesBufferList)
        {
            if (bubble != null)
            {
                int NumberOfMatched = MatchFinder.RemoveBubbleAndMatchesIfExist(bubble, ref comboMultiplier);

                if (NumberOfMatched != 0)
                {
                    FillEmptySpace();
                    return;
                }
            }
            unmatchedBubbles.Add(bubble);
        }
        recentlyMovedBubblesBufferList.RemoveAll(bubble => unmatchedBubbles.Contains(bubble));
        comboMultiplier = 1;
        CheckMatrixDeadlock();
    }
}

