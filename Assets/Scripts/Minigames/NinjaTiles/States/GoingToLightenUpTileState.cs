public class GoingToLightenUpTileState : BaseTileState, ITileState
{
    public GoingToLightenUpTileState(ColumnTile columnTile) : base(columnTile) { }

    public override void NextIteration()
    {
        columnTile.State = new LightedUpTileState(columnTile);
    }

    public override void SetToGoingToLightenUp()
    {
        // ?
    }
}
