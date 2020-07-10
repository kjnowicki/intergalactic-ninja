public class DarkTileState : BaseTileState, ITileState
{
    public DarkTileState(ColumnTile columnTile) : base(columnTile) { }

    public override bool NinjaSafe => true;

    public override void NextIteration() { }

    public override void SetToGoingToLightenUp()
    {
        columnTile.State = new GoingToLightenUpTileState(columnTile);
    }
}
