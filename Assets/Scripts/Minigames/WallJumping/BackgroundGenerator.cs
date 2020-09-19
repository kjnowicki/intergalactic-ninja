using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    private const string BACKGROUND_BLOCK_PREFAB_PATH = "Minigames/WallJumping/Prefabs/BackgroundBlock";

    private RectTransform gameAreaRectTransform;
    private Vector2 newBlockPosition;
    private GameObject backgroundBlockPrefab;


    public void Initialize(RectTransform gameAreaRectTransform)
    {
        backgroundBlockPrefab = Resources.Load<GameObject>(BACKGROUND_BLOCK_PREFAB_PATH);
        this.gameAreaRectTransform = gameAreaRectTransform;

        Bounds bounds = backgroundBlockPrefab.GetComponent<SpriteRenderer>().bounds;
        float backgroundBlockWidth = bounds.size.x;
        float gameAreaWidth = gameAreaRectTransform.sizeDelta.x;
        float backgroundBlockScale = gameAreaWidth / backgroundBlockWidth;
        backgroundBlockPrefab.transform.localScale = new Vector2(backgroundBlockScale, 1);
        //newBlockPosition = gameAreaRectTransform.localPosition;
        //backgroundBlockPrefab.transform.x
        //backgroundBlockPrefab.transform.localScale.x = 
        //float backgroundBlockHeight = backgroundBlockPrefab.GetComponent<SpriteRenderer>().bounds.size.y;

        //newBlockPosition = new Vector2(0, gameAreaRectTransform.localPosition.y);
        Scale();
    }

    private void Scale()
    {
        //GameObject backgroundTile = Instantiate(backgroundTilePrefab, tilePosition, Quaternion.identity, this.transform) as GameObject;
        Instantiate(backgroundBlockPrefab, newBlockPosition, Quaternion.identity, transform);
    }
}
