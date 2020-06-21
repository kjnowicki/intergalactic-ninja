using UnityEngine;

public class SwipeControlMethod : BaseControlMethod
{
    private float _swipeAngle = 0;

    public SwipeControlMethod(Matrix matrix) : base(matrix) { }

    /// <summary>
    /// This method changes position only in Array
    /// </summary>
    public override void MovePieces()
    {
        if (_swipeAngle != 0 && _swipeAngle > -45 && _swipeAngle <= 45 && _swipeAngle != 0 && FirstFocusedBubble.column < matrix.width - 1)
        {
            //Right swipe
            SecondFocusedBubble = matrix.MatrixBubbles[FirstFocusedBubble.column + 1, FirstFocusedBubble.row].GetComponent<Bubble>();
            SecondFocusedBubble.column -= 1;
            FirstFocusedBubble.column += 1;

        }
        else if (_swipeAngle != 0 && _swipeAngle > 45 && _swipeAngle <= 135 && _swipeAngle != 0 && FirstFocusedBubble.row < matrix.height - 1)
        {
            //Up swipe
            SecondFocusedBubble = matrix.MatrixBubbles[FirstFocusedBubble.column, FirstFocusedBubble.row + 1].GetComponent<Bubble>();
            SecondFocusedBubble.row -= 1;
            FirstFocusedBubble.row += 1;
        }
        else if (_swipeAngle != 0 && (_swipeAngle > 135 || _swipeAngle <= -135) && _swipeAngle != 0 && FirstFocusedBubble.column > 0)
        {
            //Left swipe
            SecondFocusedBubble = matrix.MatrixBubbles[FirstFocusedBubble.column - 1, FirstFocusedBubble.row].GetComponent<Bubble>();
            SecondFocusedBubble.column += 1;
            FirstFocusedBubble.column -= 1;
        }
        else if (_swipeAngle != 0 && _swipeAngle < -45 && _swipeAngle >= -135 && _swipeAngle != 0 && FirstFocusedBubble.row > 0)
        {
            //Down swipe
            SecondFocusedBubble = matrix.MatrixBubbles[FirstFocusedBubble.column, FirstFocusedBubble.row - 1].GetComponent<Bubble>();
            SecondFocusedBubble.row += 1;
            FirstFocusedBubble.row -= 1;
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
            CalculateAngle();
            MovePieces();
        }
    }

    /// <summary>
    /// This method calculate angle between touch on and "touch off" 
    /// </summary>
    private void CalculateAngle()
    {
        _swipeAngle = Mathf.Atan2(SecondTouch.GetValueOrDefault().y - FirstTouch.GetValueOrDefault().y, SecondTouch.GetValueOrDefault().x - FirstTouch.GetValueOrDefault().x) * 180 / Mathf.PI;
    }

}
