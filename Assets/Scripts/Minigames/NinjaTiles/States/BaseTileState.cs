public abstract class BaseTileState : ITileState
{
    protected ColumnTile columnTile;

    public BaseTileState(ColumnTile columnTile)
    {
        this.columnTile = columnTile;
    }

    public abstract void NextIteration();

    public abstract void SetToGoingToLightenUp();
}
