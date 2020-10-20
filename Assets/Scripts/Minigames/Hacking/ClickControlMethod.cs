using UnityEngine;

public class ClickControlMethod : BaseControlMethod
{
    public ClickControlMethod(Matrix matrix) : base(matrix) { }

    /// <summary>
    /// This method changes position only in Array
    /// </summary>
    public override void MovePieces()
    {
        if (FirstFocusedBubble.IsNeighbourOfBubble(SecondFocusedBubble))
        {
            GameManagerHacking.Instance.DisableControl();
            matrix.SwapBubbles(FirstFocusedBubble, SecondFocusedBubble);
        }
        ClearData();
    }

    public override void OnMouseDownAction(Bubble bubble)
    {
        if (!GameManagerHacking.Instance.ControlEnabled)
            return;
        if (FirstFocusedBubble == null)
        {
            FirstFocusedBubble = bubble;
            FirstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (SecondFocusedBubble == null)
        {
            SecondFocusedBubble = bubble;
            SecondTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MovePieces();
        }
    }
    public override void OnMouseUpAction() { }
}
