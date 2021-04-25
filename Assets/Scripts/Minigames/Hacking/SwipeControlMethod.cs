using UnityEngine;

public class SwipeControlMethod : BaseControlMethod
{
    public SwipeControlMethod(Matrix matrix) : base(matrix) { }

    /// <summary>
    /// This method changes position only in Array
    /// </summary>
    public override void MovePieces()
    {
        float xDif = SecondTouch.GetValueOrDefault().x - FirstTouch.GetValueOrDefault().x;
        float yDif = SecondTouch.GetValueOrDefault().y - FirstTouch.GetValueOrDefault().y;
        if (xDif == 0 && yDif == 0)
        {
            ClearData();
            return;
        }

        int colDif = 0;
        int rowDif = 0;

        if (Mathf.Abs(xDif) > Mathf.Abs(yDif)) //Check for axis
        {
            if (xDif > 0) colDif = 1;
            else colDif = -1;
        }
        else
        {
            if (yDif > 0) rowDif = 1;
            else rowDif = -1;
        }

        int newColumn = FirstFocusedBubble.column + colDif;
        int newRow = FirstFocusedBubble.row + rowDif;

        if (newColumn >= 0 && newColumn < matrix.width && newRow >= 0 && newRow < matrix.height)
        {
            SecondFocusedBubble = matrix.MatrixBubbles[newColumn, newRow].GetComponent<Bubble>();
            matrix.SwapBubbles(FirstFocusedBubble, SecondFocusedBubble);
        }
        ClearData();
    }

    public override void OnMouseDownAction(Bubble bubble)
    {
        if (GameManagerHacking.Instance.ControlEnabled && FirstTouch == null && SecondTouch == null)
        {
            FirstFocusedBubble = bubble;
            FirstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public override void OnMouseUpAction()
    {
        if (GameManagerHacking.Instance.ControlEnabled && SecondTouch == null && FirstTouch != null)
        {
            SecondTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!AreTouchesOverTheSameBubble())
            {
                MovePieces();
                return;
            }
        }
        ClearData();
    }

    private bool AreTouchesOverTheSameBubble()
    {
        return Mathf.RoundToInt(FirstTouch.Value.x) == Mathf.RoundToInt(SecondTouch.Value.x) && Mathf.RoundToInt(FirstTouch.Value.y) == Mathf.RoundToInt(SecondTouch.Value.y);
    }
}
