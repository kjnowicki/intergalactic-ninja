using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    [Header("Matrix parameters") ]
    public int width;
    public int height;
    public GameObject backgroundTilePrefab;
    public float _moveTime = 0.1f;

    private BackgroundTile[,] _matrixBackgroundTiles;

    private GameObject[] _bubblesPool; // it contains pool of unique bubbles (matrix is filled with those bubbles)

    private float _swipeAngle = 0;

    public Bubble FirstFocusedBubble { get ; set ; }
    public Bubble SecondFocusedBubble { get ; set ; }
    public Vector2? FirstTouch { get ; set ; }
    public Vector2? SecondTouch { get; set; }
    public GameObject[,] MatrixBubbles { get ; set ; }
    public HackingMethod HackingMethod { get ; set ; } = HackingMethod.Sweep;

    //dot => bubble
    void Start()
    {
        _matrixBackgroundTiles = new BackgroundTile[width, height];
        MatrixBubbles = new GameObject[width, height];
        _bubblesPool = Resources.LoadAll<GameObject>("Minigames/Hacking/Prefabs/Bubbles");
        FirstFocusedBubble = null;
        SecondFocusedBubble = null;
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
                MatrixBubbles[i, j] = backgroundTile;

                int bubbleToUseIndex = Random.Range(0, _bubblesPool.Length);
                GameObject bubble = Instantiate(_bubblesPool[bubbleToUseIndex], backgroundTilePosition, Quaternion.identity, this.transform) as GameObject;
                bubble.name = string.Format(bubbleName, i, j); ;
                MatrixBubbles[i, j] = bubble;
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

                MatrixBubbles[FirstFocusedBubble.column, FirstFocusedBubble.row] = FirstFocusedBubble.gameObject;
                MatrixBubbles[SecondFocusedBubble.column, SecondFocusedBubble.row] = SecondFocusedBubble.gameObject;

                ClearData();
            }


        }
    }
    /// <summary>
    /// This method calculate angle between touch on and "touch off" 
    /// </summary>
    public void CalculateAngle()
    {
        _swipeAngle = Mathf.Atan2(SecondTouch.GetValueOrDefault().y - FirstTouch.GetValueOrDefault().y, SecondTouch.GetValueOrDefault().x - FirstTouch.GetValueOrDefault().x) * 180 / Mathf.PI;
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
            if (FirstFocusedBubble.column < width - 1)
            {
                SecondFocusedBubble = MatrixBubbles[FirstFocusedBubble.column + 1, FirstFocusedBubble.row].GetComponent<Bubble>();
                SecondFocusedBubble.column -= 1;
                FirstFocusedBubble.column += 1;
            }
            else
                ClearData();

        }
        else if (_swipeAngle > 45 && _swipeAngle <= 135 && _swipeAngle != 0)
        {
            //Up swipe
            if (FirstFocusedBubble.row < height - 1)
            {
                SecondFocusedBubble = MatrixBubbles[FirstFocusedBubble.column, FirstFocusedBubble.row + 1].GetComponent<Bubble>();
                SecondFocusedBubble.row -= 1;
                FirstFocusedBubble.row += 1;
            }
            else
                ClearData();

        }
        else if ((_swipeAngle > 135 || _swipeAngle <= -135) && _swipeAngle != 0)
        {
            //Left swipe
            if (FirstFocusedBubble.column > 0)
            {
                SecondFocusedBubble = MatrixBubbles[FirstFocusedBubble.column - 1, FirstFocusedBubble.row].GetComponent<Bubble>();
                SecondFocusedBubble.column += 1;
                FirstFocusedBubble.column -= 1;
            }
            else
                ClearData();
        }
        else if (_swipeAngle < -45 && _swipeAngle >= -135 && _swipeAngle != 0)
        {
            //Down swipe
            if (FirstFocusedBubble.row > 0)
            {
                SecondFocusedBubble = MatrixBubbles[FirstFocusedBubble.column, FirstFocusedBubble.row - 1].GetComponent<Bubble>();
                SecondFocusedBubble.row += 1;
                FirstFocusedBubble.row -= 1;
            }
            else
                ClearData();
        }
        else if (_swipeAngle == 0)
            ClearData();

    }

    public void MovePiecesClick()
    {
        if ((Mathf.Abs(FirstFocusedBubble.row - SecondFocusedBubble.row) == 1 && Mathf.Abs(FirstFocusedBubble.column - SecondFocusedBubble.column) == 0) ||
            (Mathf.Abs(FirstFocusedBubble.row - SecondFocusedBubble.row) == 0 && Mathf.Abs(FirstFocusedBubble.column - SecondFocusedBubble.column) == 1))
        {
            int tmpRow = SecondFocusedBubble.row;
            int tmpColumn = SecondFocusedBubble.column;

            SecondFocusedBubble.row = FirstFocusedBubble.row;
            FirstFocusedBubble.row = tmpRow;

            SecondFocusedBubble.column = FirstFocusedBubble.column;
            FirstFocusedBubble.column = tmpColumn;
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
        FirstFocusedBubble = null;
        SecondFocusedBubble = null;
        FirstTouch = null;
        SecondTouch = null;
    }


}

public enum HackingMethod
{
    Sweep,
    Click

}