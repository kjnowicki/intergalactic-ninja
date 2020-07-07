using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingToLightenUpTileState : BaseTileState, ITileState
{
    public GoingToLightenUpTileState(ColumnTile columnTile) : base(columnTile) { }

    public override void Darken()
    {
        
    }

    public override void LightenUp()
    {
        columnTile.State = new LightedUpTileState(columnTile);
    }

    public override void SetToGoingToLightenUp()
    {
        
    }
}
