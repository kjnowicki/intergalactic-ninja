using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightedUpTileState : BaseTileState, ITileState
{
    public LightedUpTileState(ColumnTile columnTile) : base(columnTile) { }

    public override void Darken()
    {
        columnTile.State = new DarkTileState(columnTile);
    }

    public override void LightenUp()
    {
        
    }

    public override void SetToGoingToLightenUp()
    {
        
    }
}
