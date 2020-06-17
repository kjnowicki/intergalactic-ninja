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

    //dot => bubble
    void Start()
    {
        _matrixBackgroundTiles = new BackgroundTile[width, height];
        _matrixBubbles = new GameObject[width, height];
        _bubblesPool = Resources.LoadAll<GameObject>("Minigames/Hacking/Prefabs/Bubbles");
        SetUp();
    }

    private void SetUp()
    {
        string backgroundTileName = "Tile ( {0},{1} )";
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
                _matrixBubbles[i, j]= bubble;
            }
        }

    }
}
