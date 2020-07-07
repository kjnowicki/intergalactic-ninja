using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTileState : ITileState
{
    protected ColumnTile columnTile;

    public BaseTileState(ColumnTile columnTile)
    {
        this.columnTile = columnTile;
    }

    public abstract void Darken();

    public abstract void LightenUp();

    public abstract void SetToGoingToLightenUp();
}
