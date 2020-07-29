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
        //normalWallBlockPrefab.
        var screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        //leftWall.Position = 
        float leftBorder = -screenBounds.x + 0.5f;
        float rightBorder = screenBounds.x - 0.5f;

        GameObject block = Instantiate(normalWallBlockPrefab, new Vector2(rightBorder, 0), Quaternion.identity, transform);
        GameObject block2 = Instantiate(normalWallBlockPrefab, new Vector2(leftBorder, 0), Quaternion.identity, transform);

        //leftWall.gameObject.
    }

    // Update is called once per frame
    void Update()
    {

    }
}
