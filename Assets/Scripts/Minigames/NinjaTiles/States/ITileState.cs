public interface ITileState
{
    bool NinjaSafe { get; }

    void SetToGoingToLightenUp();

    void NextIteration();
}
