using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColumnTile : MonoBehaviour
{
    private void OnMouseDown()
    {
        MoveToTouchedColumn(gameObject);
    }

    /// <summary>
    /// On click event instructions for column tile
    /// </summary>
    /// <param name="columnTile"></param>
    internal static void MoveToTouchedColumn(GameObject columnTile)
    {
        Debug.Log("Ninja will move to column Tile: " + columnTile.name);
    }
}
