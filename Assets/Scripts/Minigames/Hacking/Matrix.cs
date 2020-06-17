using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject backgroundTilePrefab;

    private BackgroundTile[,] _matrixBackgroundTiles;

    private GameObject[] _bubblesPool; // it contains pool of unique bubbles (matrix is filled with those bubbles)
    public GameObject[,] _matrixBubbles;

    public Bubble firstFocusedBubble;
    public Bubble secondFocusedBubble;
    public Vector2? firstTouch;
    public Vector2? secondTouch;
    private float _swipeAngle = 0;
    private readonly float _moveTime = 0.1f;

    public HackingMethod hackingMethod = HackingMethod.Sweep;

    //dot => bubble
    void Start()
    {
        _matrixBackgroundTiles = new BackgroundTile[width, height];
        _matrixBubbles = new GameObject[width, height];
        _bubblesPool = Resources.LoadAll<GameObject>("Minigames/Hacking/Prefabs/Bubbles");
        firstFocusedBubble = null;
        secondFocusedBubble = null;
        SetUp();
    }
    /// <summary>
    /// Method is setting all Matrix with BackgroundTiles (unmovable) and bubbles (movable)
    /// </summary>
    private void SetUp()
    {
        string backgroundTileName = "BackgroundTile ( {0},{1} )";
        string bubbleName = "Bubble ( {0},{1} )";

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 backgroundTilePosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(backgroundTilePrefab, backgroundTilePosition, Quaternion.identity, this.transform) as GameObject;
                backgroundTile.name = string.Format(backgroundTileName, i, j);
                _matrixBubbles[i, j] = backgroundTile;

                int bubbleToUseIndex = Random.Range(0, _bubblesPool.Length);
                GameObject bubble = Instantiate(_bubblesPool[bubbleToUseIndex], backgroundTilePosition, Quaternion.identity, this.transform) as GameObject;
                bubble.name = string.Format(bubbleName, i, j); ;
                _matrixBubbles[i, j] = bubble;
            }
        }
    }

    private void FixedUpdate()
    {
        ChangePosition();
    }
    /// <summary>
    /// Method after Bubble swap is changing position by interpolation. Array is already changed.
    /// </summary>
    private void ChangePosition()
    {
        if (firstFocusedBubble != null && secondFocusedBubble != null && firstTouch != null && secondTouch != null)
        {

            if (Mathf.Abs(firstFocusedBubble.transform.position.x - firstFocusedBubble.column) > _moveTime ||
                Mathf.Abs(firstFocusedBubble.transform.position.y - firstFocusedBubble.row) > _moveTime ||
                Mathf.Abs(secondFocusedBubble.transform.position.x - secondFocusedBubble.column) > _moveTime ||
                Mathf.Abs(secondFocusedBubble.transform.position.y - secondFocusedBubble.row) > _moveTime)
            {
                Vector2 tempPosition1 = new Vector2(firstFocusedBubble.column, firstFocusedBubble.row);
                firstFocusedBubble.transform.position = Vector2.MoveTowards(firstFocusedBubble.transform.position, tempPosition1, _moveTime);

                Vector2 tempPosition2 = new Vector2(secondFocusedBubble.column, secondFocusedBubble.row);
                secondFocusedBubble.transform.position = Vector2.MoveTowards(secondFocusedBubble.transform.position, tempPosition2, _moveTime);
            }
            else
            {
                firstFocusedBubble.transform.position = new Vector2(firstFocusedBubble.column, firstFocusedBubble.row);
                secondFocusedBubble.transform.position = new Vector2(secondFocusedBubble.column, secondFocusedBubble.row);

                _matrixBubbles[firstFocusedBubble.column, firstFocusedBubble.row] = firstFocusedBubble.gameObject;
                _matrixBubbles[secondFocusedBubble.column, secondFocusedBubble.row] = secondFocusedBubble.gameObject;

                ClearData();
            }


        }
    }
    /// <summary>
    /// This method calculate angle between touch on and "touch off" 
    /// </summary>
    public void CalculateAngle()
    {
        _swipeAngle = Mathf.Atan2(secondTouch.GetValueOrDefault().y - firstTouch.GetValueOrDefault().y, secondTouch.GetValueOrDefault().x - firstTouch.GetValueOrDefault().x) * 180 / Mathf.PI;
        MovePiecesSweep();
    }

    /// <summary>
    /// This method change position by sweep only in Array
    /// </summary>
    private void MovePiecesSweep()
    {
        if (_swipeAngle > -45 && _swipeAngle <= 45 && _swipeAngle != 0)
        {
            //Right swipe
            if (firstFocusedBubble.column < width - 1)
            {
                secondFocusedBubble = _matrixBubbles[firstFocusedBubble.column + 1, firstFocusedBubble.row].GetComponent<Bubble>();
                secondFocusedBubble.column -= 1;
                firstFocusedBubble.column += 1;
            }
            else
                ClearData();

        }
        else if (_swipeAngle > 45 && _swipeAngle <= 135 && _swipeAngle != 0)
        {
            //Up swipe
            if (firstFocusedBubble.row < height - 1)
            {
                secondFocusedBubble = _matrixBubbles[firstFocusedBubble.column, firstFocusedBubble.row + 1].GetComponent<Bubble>();
                secondFocusedBubble.row -= 1;
                firstFocusedBubble.row += 1;
            }
            else
                ClearData();

        }
        else if ((_swipeAngle > 135 || _swipeAngle <= -135) && _swipeAngle != 0)
        {
            //Left swipe
            if (firstFocusedBubble.column > 0)
            {
                secondFocusedBubble = _matrixBubbles[firstFocusedBubble.column - 1, firstFocusedBubble.row].GetComponent<Bubble>();
                secondFocusedBubble.column += 1;
                firstFocusedBubble.column -= 1;
            }
            else
                ClearData();
        }
        else if (_swipeAngle < -45 && _swipeAngle >= -135 && _swipeAngle != 0)
        {
            //Down swipe
            if (firstFocusedBubble.row > 0)
            {
                secondFocusedBubble = _matrixBubbles[firstFocusedBubble.column, firstFocusedBubble.row - 1].GetComponent<Bubble>();
                secondFocusedBubble.row += 1;
                firstFocusedBubble.row -= 1;
            }
            else
                ClearData();
        }
        else if (_swipeAngle == 0)
            ClearData();

    }

    public void MovePiecesClick()
    {
        if ((Mathf.Abs(firstFocusedBubble.row - secondFocusedBubble.row) == 1 && Mathf.Abs(firstFocusedBubble.column - secondFocusedBubble.column) == 0) ||
            (Mathf.Abs(firstFocusedBubble.row - secondFocusedBubble.row) == 0 && Mathf.Abs(firstFocusedBubble.column - secondFocusedBubble.column) == 1))
        {
            int tmpRow = secondFocusedBubble.row;
            int tmpColumn = secondFocusedBubble.column;

            secondFocusedBubble.row = firstFocusedBubble.row;
            firstFocusedBubble.row = tmpRow;

            secondFocusedBubble.column = firstFocusedBubble.column;
            firstFocusedBubble.column = tmpColumn;
        }
        else 
        {
            ClearData();
        }
    }
    /// <summary>
    ///  Method which clear data. Depending on this data, the next move is captured.
    /// </summary>
    private void ClearData()
    {
        firstFocusedBubble = null;
        secondFocusedBubble = null;
        firstTouch = null;
        secondTouch = null;
    }


}

public enum HackingMethod
{
    Sweep,
    Click

}