public class LightedUpTileState : BaseTileState, ITileState
{
    public LightedUpTileState(ColumnTile columnTile) : base(columnTile) { }

    public override bool NinjaSafe => false;

    public override void NextIteration()
    {
        columnTile.State = new DarkTileState(columnTile);
    }

    public override void SetToGoingToLightenUp() {} // Do nothing or do if it is called before NextIteration() (or after idk)
}
