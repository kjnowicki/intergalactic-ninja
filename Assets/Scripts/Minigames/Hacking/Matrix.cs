using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    private BackgroundTile[,] _allTiles;

    void Start()
    {
        _allTiles = new BackgroundTile[width, height];
        SetUp();
    }

    private void SetUp()
    {
        string backgroundTileName = "( {0},{1} )";
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 backgroundTilePosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, backgroundTilePosition, Quaternion.identity, this.transform) as GameObject;
                backgroundTile.name = string.Format(backgroundTileName, i, j);
            }
        }

    }
}
