using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Mathf;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    public int column;
    public int row;
    private Matrix _matrix;

    // Start is called before the first frame update
    void Start()
    {
        _matrix = FindObjectOfType<Matrix>();
        column = (int)transform.position.x;
        row = (int)transform.position.y;
    }

    private void OnMouseDown()
    {
        _matrix.controlMethod.OnMouseDownAction(this);
    }

    private void OnMouseUp()
    {
        _matrix.controlMethod.OnMouseUpAction();
    }

    public bool IsMoving(float moveTime) 
    {
        return Abs(transform.position.x - column) > moveTime || Abs(transform.position.y - row) > moveTime;
    }

    public bool IsNeighbourOfBubble(Bubble secondBubble) 
    {
        return (Mathf.Abs(row - secondBubble.row) == 1 && Mathf.Abs(column - secondBubble.column) == 0) ||
                (Mathf.Abs(row - secondBubble.row) == 0 && Mathf.Abs(column - secondBubble.column) == 1);
    }

}
