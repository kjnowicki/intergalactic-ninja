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

        if (_matrix.FirstTouch == null && _matrix.SecondTouch == null && _matrix.HackingMethod == HackingMethod.Sweep)
        {
            _matrix.FirstFocusedBubble = this;
            _matrix.FirstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
        else if (_matrix.HackingMethod == HackingMethod.Click)
        {
            if (_matrix.FirstFocusedBubble == null)
            {
                _matrix.FirstFocusedBubble = this;
                _matrix.FirstTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (_matrix.SecondFocusedBubble == null)
            {
                _matrix.SecondFocusedBubble = this;
                _matrix.SecondTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _matrix.MovePiecesClick();
            }
        }
    }

    private void OnMouseUp()
    {
        if (_matrix.SecondTouch == null && _matrix.FirstTouch != null && _matrix.HackingMethod == HackingMethod.Sweep)
        {
            _matrix.SecondTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _matrix.CalculateAngle();

        }

    }




}
