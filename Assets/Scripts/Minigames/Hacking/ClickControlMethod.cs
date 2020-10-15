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
            int tmpRow = SecondFocusedBubble.row;
            int tmpColumn = SecondFocusedBubble.column;

            SecondFocusedBubble.row = FirstFocusedBubble.row;
            FirstFocusedBubble.row = tmpRow;

            SecondFocusedBubble.column = FirstFocusedBubble.column;
            FirstFocusedBubble.column = tmpColumn;
        }
        else
        {
            ClearData();
        }
    }

    public override void OnMouseDownAction(Bubble bubble)
    {
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
