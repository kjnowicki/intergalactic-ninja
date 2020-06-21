using System.Collections;
using System.Collections.Generic;
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
        _matrix.moveMethod.OnMouseDownAction(this);
    }

    private void OnMouseUp()
    {
        _matrix.moveMethod.OnMouseUpAction();
    }




}
