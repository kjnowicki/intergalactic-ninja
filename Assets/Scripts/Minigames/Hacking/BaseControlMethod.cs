using UnityEngine;


public abstract class BaseControlMethod
{
    public Bubble FirstFocusedBubble { get; set; }
    public Bubble SecondFocusedBubble { get; set; }
    public Vector2? FirstTouch { get; set; }
    public Vector2? SecondTouch { get; set; }

    public float _moveTime = 0.1f;

    protected Matrix matrix;

    protected BaseControlMethod(Matrix matrix)
    {
        this.matrix = matrix;
        FirstFocusedBubble = null;
        SecondFocusedBubble = null;
    }

    /// <summary>
    /// Method which is after MovePieces().It is changing position by interpolation. Array is already changed.
    /// </summary>
    public virtual void ChangePosition()
    {
        if (FirstFocusedBubble != null && SecondFocusedBubble != null && FirstTouch != null && SecondTouch != null)
        {
            Vector2 firstBubbleCellPosition = new Vector2(FirstFocusedBubble.column, FirstFocusedBubble.row);
            Vector2 secondBubbleCellPosition = new Vector2(SecondFocusedBubble.column, SecondFocusedBubble.row);

            if (FirstFocusedBubble.IsMoving(_moveTime) || SecondFocusedBubble.IsMoving(_moveTime))
            {
                FirstFocusedBubble.transform.position = Vector2.MoveTowards(FirstFocusedBubble.transform.position, firstBubbleCellPosition, _moveTime);
                SecondFocusedBubble.transform.position = Vector2.MoveTowards(SecondFocusedBubble.transform.position, secondBubbleCellPosition, _moveTime);
            }
            else
            {
                FirstFocusedBubble.transform.position = firstBubbleCellPosition;
                SecondFocusedBubble.transform.position = secondBubbleCellPosition;

                matrix.MatrixBubbles[FirstFocusedBubble.column, FirstFocusedBubble.row] = FirstFocusedBubble.gameObject;
                matrix.MatrixBubbles[SecondFocusedBubble.column, SecondFocusedBubble.row] = SecondFocusedBubble.gameObject;

                matrix.CheckMatrixDeadlock();
                ClearData();
            }
        }
    }

    /// <summary>
    ///  Method which clears data. Depending on this data, the next move is captured.
    /// </summary>
    protected void ClearData()
    {
        FirstFocusedBubble = null;
        SecondFocusedBubble = null;
        FirstTouch = null;
        SecondTouch = null;
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

