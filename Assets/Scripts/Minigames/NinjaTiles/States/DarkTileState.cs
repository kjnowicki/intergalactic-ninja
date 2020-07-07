using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkTileState : BaseTileState, ITileState
{
    public DarkTileState(ColumnTile columnTile) : base(columnTile) { }

    public override void Darken()
    {
        
    }

    public override void LightenUp()
    {
        
    }

    public override void SetToGoingToLightenUp()
    {
        columnTile.State = new GoingToLightenUpTileState(columnTile);
    }
}
