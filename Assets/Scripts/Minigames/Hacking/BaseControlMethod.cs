using UnityEngine;


public abstract class BaseControlMethod
{
    public Bubble FirstFocusedBubble { get; set; }
    public Bubble SecondFocusedBubble { get; set; }
    public Vector2? FirstTouch { get; set; }
    public Vector2? SecondTouch { get; set; }

    protected Matrix matrix;

    protected BaseControlMethod(Matrix matrix)
    {
        this.matrix = matrix;
        FirstFocusedBubble = null;
        SecondFocusedBubble = null;
        GameManagerHacking.Instance.EnableControl();
    }

    /// <summary>
    ///  Method which clears data. Depending on this data, the next move is captured.
    /// </summary>
    public void ClearData()
    {
        FirstTouch = null;
        SecondTouch = null;
        FirstFocusedBubble = null;
        SecondFocusedBubble = null;
    }

    /// <summary>
    /// This method changes position only in Array
    /// </summary>
    public abstract void MovePieces();
    public abstract void OnMouseDownAction(Bubble bubble);
    public abstract void OnMouseUpAction();

}
public enum ControlMethod
{
    Swipe,
    Click
}

