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

    // Update is called once per frame

    private void OnMouseDown()
    {

        if (_matrix.firstTouch == null && _matrix.secondTouch == null)
        {
            _matrix.firstFocusedBubble = this;
            _matrix.firstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        }
    }

    private void OnMouseUp()
    {
        if (_matrix.secondTouch == null && _matrix.firstTouch != null)
        {
            _matrix.secondTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _matrix.CalculateAngle();
        }
  
    }




}
