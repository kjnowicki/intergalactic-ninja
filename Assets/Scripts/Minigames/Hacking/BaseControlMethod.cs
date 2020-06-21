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

            if (Mathf.Abs(FirstFocusedBubble.transform.position.x - FirstFocusedBubble.column) > _moveTime ||
                Mathf.Abs(FirstFocusedBubble.transform.position.y - FirstFocusedBubble.row) > _moveTime ||
                Mathf.Abs(SecondFocusedBubble.transform.position.x - SecondFocusedBubble.column) > _moveTime ||
                Mathf.Abs(SecondFocusedBubble.transform.position.y - SecondFocusedBubble.row) > _moveTime)
            {
                Vector2 tempPosition1 = new Vector2(FirstFocusedBubble.column, FirstFocusedBubble.row);
                FirstFocusedBubble.transform.position = Vector2.MoveTowards(FirstFocusedBubble.transform.position, tempPosition1, _moveTime);

                Vector2 tempPosition2 = new Vector2(SecondFocusedBubble.column, SecondFocusedBubble.row);
                SecondFocusedBubble.transform.position = Vector2.MoveTowards(SecondFocusedBubble.transform.position, tempPosition2, _moveTime);
            }
            else
            {
                FirstFocusedBubble.transform.position = new Vector2(FirstFocusedBubble.column, FirstFocusedBubble.row);
                SecondFocusedBubble.transform.position = new Vector2(SecondFocusedBubble.column, SecondFocusedBubble.row);

                matrix.MatrixBubbles[FirstFocusedBubble.column, FirstFocusedBubble.row] = FirstFocusedBubble.gameObject;
                matrix.MatrixBubbles[SecondFocusedBubble.column, SecondFocusedBubble.row] = SecondFocusedBubble.gameObject;

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

public static class  ControlMethodFactory
{
    /// <summary>
    /// This Method gets control method from PlayerPrefs and return it 
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static BaseControlMethod GetControlMethod( Matrix matrix) 
    {
        ControlMethod controlMethod = PlayerPrefsWrapper.GetHackingControlMethod();
        BaseControlMethod result;
        switch (controlMethod)
        {
            case ControlMethod.Swipe:
                result = new SwipeControlMethod(matrix);
                break;
            case ControlMethod.Click:
                result = new ClickControlMethod(matrix);
                break;
            default:
               result = new SwipeControlMethod(matrix);
                break;
        }
            return result;
    }

}
public enum ControlMethod
{
   Swipe,
   Click
}

