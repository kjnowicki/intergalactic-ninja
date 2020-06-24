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
            SwapCoords(FirstFocusedBubble, SecondFocusedBubble); //method exchanging row and column field between the bubbles
        }
        else
            ClearData();

    }

    public override void OnMouseDownAction(Bubble bubble)
    {
        if (FirstTouch == null && SecondTouch == null)
        {
            FirstFocusedBubble = bubble;
            FirstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public override void OnMouseUpAction()
    {
        if (SecondTouch == null && FirstTouch != null)
        {
            SecondTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MovePieces();
        }
    }

    private void SwapCoords(Bubble firstFocusedBubble, Bubble secondFocusedBubble)
    {
        int row = firstFocusedBubble.row;
        int column = firstFocusedBubble.column;

        firstFocusedBubble.row = secondFocusedBubble.row;
        firstFocusedBubble.column = secondFocusedBubble.column;

        secondFocusedBubble.row = row;
        secondFocusedBubble.column = column;
    }

}
