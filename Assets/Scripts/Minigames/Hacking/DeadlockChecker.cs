using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static UnityEngine.Mathf;

public class DeadlockChecker
{
    private const int DEADLOCK_EXPECTED_NUMBER_OF_NEIHBOURS = 3;

    private const int DEADLOCK_NEIGHBOUR_RANGE = 2;

    private Matrix matrix;

    public DeadlockChecker(Matrix matrix)
    {
        this.matrix = matrix;
    }

    /// <summary>
    /// Checks if there are no moves to be made for current matrix
    /// </summary>
    /// <returns>True if there are no moves, False if there is a move</returns>
    public bool IsMatrixOnDeadlock()
    {
        for (int j = 0; j < matrix.height; j++)
        {
            for (int i = 0; i < matrix.width; i++)
            {
                if (IsMovePossibleAtPoint(i, j))
                {
                    Debug.Log(string.Format("Possible at point ({0},{1}).", i, j));
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
        for (int k = Max(0, col - DEADLOCK_NEIGHBOUR_RANGE); k < Min(matrix.width, col + DEADLOCK_NEIGHBOUR_RANGE + 1); k++)
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
        for (int k = Max(0, row - DEADLOCK_NEIGHBOUR_RANGE); k < Min(matrix.height, row + DEADLOCK_NEIGHBOUR_RANGE + 1); k++)
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
        return matrix.MatrixBubbles[i, j].GetComponent<Bubble>().color;
    }
}
