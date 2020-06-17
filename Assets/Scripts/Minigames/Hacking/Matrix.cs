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
    public float swipeAngle = 0;
    private float time = 0.1f;

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

    private void ChangePosition()
    {
        if (firstFocusedBubble != null && secondFocusedBubble != null && firstTouch != null && secondTouch != null)
        {

            if (Mathf.Abs(firstFocusedBubble.transform.position.x - firstFocusedBubble.column) > time ||
                Mathf.Abs(firstFocusedBubble.transform.position.y - firstFocusedBubble.row) > time ||
                Mathf.Abs(secondFocusedBubble.transform.position.x - secondFocusedBubble.column) > time ||
                Mathf.Abs(secondFocusedBubble.transform.position.y - secondFocusedBubble.row) > time)
            {
                Vector2 tempPosition1 = new Vector2(firstFocusedBubble.column, firstFocusedBubble.row);
                firstFocusedBubble.transform.position = Vector2.MoveTowards(firstFocusedBubble.transform.position, tempPosition1, time);

                Vector2 tempPosition2 = new Vector2(secondFocusedBubble.column, secondFocusedBubble.row);
                secondFocusedBubble.transform.position = Vector2.MoveTowards(secondFocusedBubble.transform.position, tempPosition2, time);
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
    public void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(secondTouch.GetValueOrDefault().y - firstTouch.GetValueOrDefault().y, secondTouch.GetValueOrDefault().x - firstTouch.GetValueOrDefault().x) * 180 / Mathf.PI;
        MovePieces();
    }

    private void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && swipeAngle != 0)
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
        else if (swipeAngle > 45 && swipeAngle <= 135 && swipeAngle != 0)
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
        else if ((swipeAngle > 135 || swipeAngle <= -135) && swipeAngle != 0)
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
        else if (swipeAngle < -45 && swipeAngle >= -135 && swipeAngle != 0)
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
        else if (swipeAngle == 0)
            ClearData();





    }

    private void ClearData()
    {
        firstFocusedBubble = null;
        secondFocusedBubble = null;
        firstTouch = null;
        secondTouch = null;
    }

}
