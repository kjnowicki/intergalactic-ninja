using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColumnTile : MonoBehaviour
{
    public ITileState State { get; set; }
    void Start()
    {
        State = new DarkTileState(this); // initial state
    }

    private void OnMouseDown()
    {
        MoveToTouchedColumn(gameObject);
    }

    /// <summary>
    /// On click event instructions for column tile
    /// </summary>
    /// <param name="columnTile"></param>
    private void MoveToTouchedColumn(GameObject columnTile)
    {
        Debug.Log("Ninja will move to column Tile: " + columnTile.name);
        NinjaTilesGameManager.Instance.MoveNinjaToTile(columnTile);
    }

    /// <summary>
    /// Invokes next iteration in current state.
    /// </summary>
    public void NextIteration()
    {
        State.NextIteration();
    }

    /// <summary>
    /// Marks current state as GOING_TO_LIGHTEN_UP
    /// </summary>
    public void SetToGoingToLightenUp()
    {
        State.SetToGoingToLightenUp();
    }
}
