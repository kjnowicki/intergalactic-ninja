public class LightedUpTileState : BaseTileState, ITileState
{
    public LightedUpTileState(ColumnTile columnTile) : base(columnTile) { }

    public override void NextIteration()
    {
        columnTile.State = new DarkTileState(columnTile);
    }

    public override void SetToGoingToLightenUp() {} // Do nothing
}
