using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public GameObject normalWallBlockPrefab;
    public Wall leftWall;
    public Wall rightWall;
    public Camera camera;

    //public int rightBorder;
    //public int leftBorder;
    //public Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        SetUp();
        //GameObject block = Instantiate(normalWallBlockPrefab, new Vector2(rightWallPosition, 0), Quaternion.identity, transform);
        //GameObject block2 = Instantiate(normalWallBlockPrefab, new Vector2(leftWallPosition, 0), Quaternion.identity, transform);
    }

    private void SetUp()
    {
        float wallBlockWidth = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float wallBlockHeight = normalWallBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float bottomWallYCoord = -screenBounds.y + wallBlockHeight / 2;
        Vector2 leftWallInitialPosition = new Vector2(-screenBounds.x + wallBlockWidth / 2, bottomWallYCoord);
        Vector2 rightWallInitialPosition = new Vector2(screenBounds.x + wallBlockWidth / 2, bottomWallYCoord);

        leftWall.Initialize(leftWallInitialPosition);
        rightWall.Initialize(rightWallInitialPosition);

        GameObject block = Instantiate(normalWallBlockPrefab, leftWallInitialPosition, Quaternion.identity, transform);
    }

    private void GenerateInitialWall()
    {
        //leftWall
    }

    // Update is called once per frame
    void Update()
    {

    }
}
