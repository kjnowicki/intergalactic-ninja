using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using static UnityEngine.Random;
using static UnityEngine.Mathf;

public class Matrix : MonoBehaviour
{
    [Header("Matrix parameters")]
    public int width;
    public int height;
    public GameObject backgroundTilePrefab;

    private GameObject[,] _matrixBackgroundTiles;

    private GameObject[] _bubblesPool; // it contains pool of unique bubbles (matrix is filled with those bubbles)

    private readonly string _bubblesAssetPath = "Minigames/Hacking/Prefabs/Bubbles";

    public GameObject[,] MatrixBubbles { get; set; }

    public BaseControlMethod controlMethod;

    private const int DEADLOCK_EXPECTED_NUMBER_OF_NEIHBOURS = 3;

    private const int DEADLOCK_NEIGHBOUR_RANGE = 2;

    private const string BACKGROUND_TILE_NAME = "BackgroundTile ({0}, {1})";

    private const string BUBBLE_NAME = "Bubble ({0}, {1})";

    //dot => bubble
    void Start()
    {
        _matrixBackgroundTiles = new GameObject[width, height];
        MatrixBubbles = new GameObject[width, height];
        _bubblesPool = Resources.LoadAll<GameObject>(_bubblesAssetPath);
        controlMethod = ControlMethodFactory.GetControlMethod(this);

        SetUp();
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

    private void FixedUpdate()
    {
        controlMethod.ChangePosition();
    }

    /// <summary>
    /// Instantiates a background tile at position
    /// </summary>
    /// <param name="tilePosition"></param>
    private void SetBackgroundTileAtPosition(Vector2 tilePosition)
    {
        GameObject backgroundTile = Instantiate(backgroundTilePrefab, tilePosition, Quaternion.identity, this.transform) as GameObject;
        backgroundTile.name = string.Format(BACKGROUND_TILE_NAME, (int)tilePosition.x, (int)tilePosition.y);
        _matrixBackgroundTiles[(int)tilePosition.x, (int)tilePosition.y] = backgroundTile;
    }

    /// <summary>
    /// Instantiates a bubble at given position
    /// </summary>
    /// <param name="tilePosition"></param>
    private void SetBubbleAtPosition(Vector2 tilePosition)
    {
        int bubbleToUseIndex = Range(0, _bubblesPool.Length);
        GameObject bubble = Instantiate(_bubblesPool[bubbleToUseIndex], tilePosition, Quaternion.identity, this.transform) as GameObject;
        bubble.name = string.Format(BUBBLE_NAME, (int)tilePosition.x, (int)tilePosition.y); ;
        MatrixBubbles[(int)tilePosition.x, (int)tilePosition.y] = bubble;
    }

    /// <summary>
    /// Checks if matrix is on deadlock. If it is the matrix gets resetted
    /// </summary>
    public void CheckMatrixDeadlock()
    {
        if (IsMatrixOnDeadlock())
        {
            Debug.Log("Matrix on deadlock. Resetting...");
            Reset();
        }
    }

    /// <summary>
    /// Resets the matrix and setups it from scratch
    /// </summary>
    private void Reset()
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
    /// Checks if there are no moves to be made for current matrix
    /// </summary>
    /// <returns>True if there are no moves, False if there is a move</returns>
    private bool IsMatrixOnDeadlock()
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (IsMovePossibleAtPoint(i, j))
                {
                    Debug.Log(String.Format("Possible at point ({0},{1}).", i, j));
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Check if the point in matrix is a target of a possible move
    /// </summary>
    /// <param name="i">Column</param>
    /// <param name="j">Row</param>
    /// <returns>True if there is a move, False if there are no moves</returns>
    private bool IsMovePossibleAtPoint(int i, int j)
    {
        List<Color> neighboursColorsList = GetNeighboursOfPoint(i, j);
        foreach (Color matchColor in neighboursColorsList.Distinct())
        {
            if (HasColorsListExpectedNumberOfColor(neighboursColorsList, matchColor, DEADLOCK_EXPECTED_NUMBER_OF_NEIHBOURS)) return true;
        }
        return false;
    }

    /// <summary>
    /// Gets list of neighbours at specific point in matrix
    /// </summary>
    /// <param name="i">Column</param>
    /// <param name="j">Row</param>
    /// <returns>List of horizontal and vertical neighbours</returns>
    private List<Color> GetNeighboursOfPoint(int i, int j)
    {
        List<Color> colorsList = new List<Color>();
        AddVerticalNeighboursOfBubbleAtPointToList(ref colorsList, i, j);
        AddHorizontalNeighboursOfBubbleAtPointToList(ref colorsList, i, j);
        return colorsList;
    }

    /// <summary>
    /// Checks if the list of colors has countity more than expected number
    /// </summary>
    /// <param name="colorsList">List of colors</param>
    /// <param name="matchColor">Color to be matched</param>
    /// <returns>True if number is equal or greater than expected number and false otherwise</returns>
    private bool HasColorsListExpectedNumberOfColor(List<Color> colorsList, Color matchColor, int expectedNumber)
    {
        if (colorsList.Where(color => color == matchColor).Count() >= expectedNumber) return true;
        else return false;
    }

    /// <summary>
    /// Gets list of vertical neighbours at specific point in matrix
    /// </summary>
    /// <param name="colorsList">List of colors</param>
    /// <param name="col">Column</param>
    /// <param name="row">Row</param>
    private void AddVerticalNeighboursOfBubbleAtPointToList(ref List<Color> colorsList, int col, int row)
    {
        for (int k = Max(0, col - DEADLOCK_NEIGHBOUR_RANGE); k < Min(width, col + DEADLOCK_NEIGHBOUR_RANGE + 1); k++)
        {
            int colDifference = k - col;
            switch (Abs(colDifference))
            {
                case 0:
                    break;
                case 1:
                    colorsList.Add(GetCellColor(k, row));
                    break;
                case 2:
                    Color cellColor = GetCellColor(k, row);
                    if (cellColor == GetCellColor(k - (int)Sign(colDifference), row)) colorsList.Add(cellColor);
                    break;
                default:
                    Debug.LogError("Neighbour out of range!");
                    break;
            }
        }
    }

    /// <summary>
    /// Gets list of horizontal neighbours at specific point in matrix
    /// </summary>
    /// <param name="colorsList">List of colors</param>
    /// <param name="col">Column</param>
    /// <param name="row">Row</param>
    private void AddHorizontalNeighboursOfBubbleAtPointToList(ref List<Color> colorsList, int col, int row)
    {
        for (int k = Max(0, row - DEADLOCK_NEIGHBOUR_RANGE); k < Min(height, row + DEADLOCK_NEIGHBOUR_RANGE + 1); k++)
        {
            int rowDiffernce = k - row;
            switch (Abs(rowDiffernce))
            {
                case 0:
                    break;
                case 1:
                    colorsList.Add(GetCellColor(col, k));
                    break;
                case 2:
                    Color cellColor = GetCellColor(col, k);
                    if (cellColor == GetCellColor(col, k - (int)Sign(rowDiffernce))) colorsList.Add(cellColor);
                    break;
                default:
                    Debug.LogError("Neighbour out of range!");
                    break;
            }
        }
    }

    /// <summary>
    /// Gets color of the cell located at (i,j) point in matrix
    /// </summary>
    /// <param name="i">Column</param>
    /// <param name="j">Row</param>
    /// <returns>Color of the cell</returns>
    private Color GetCellColor(int i, int j)
    {
        return MatrixBubbles[i, j].GetComponentInChildren<SpriteRenderer>().color;
    }
}

