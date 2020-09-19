using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    private const string BACKGROUND_BLOCK_PREFAB_PATH = "Minigames/WallJumping/Prefabs/BackgroundBlock";

    private Queue<GameObject> backgroundBlocks = new Queue<GameObject>();
    private Vector2 newBlockPosition;
    private GameObject backgroundBlockPrefab;
    private float backgroundBlockHeight;
    private int backgroundWallHeight;

    public void Initialize(RectTransform gameAreaRectTransform, int wallHeightAdditional)
    {
        backgroundBlockPrefab = Resources.Load<GameObject>(BACKGROUND_BLOCK_PREFAB_PATH);
        Vector2 backgroundBlockSize = backgroundBlockPrefab.GetComponent<SpriteRenderer>().bounds.size;
        backgroundBlockHeight = backgroundBlockSize.y;
        backgroundWallHeight = (int)(gameAreaRectTransform.rect.height / backgroundBlockHeight) + wallHeightAdditional;

        RescaleBackgroundBlock(gameAreaRectTransform.sizeDelta.x, backgroundBlockSize.x);
        SetInitialPosition(gameAreaRectTransform, backgroundBlockHeight);
        GenerateInitialBackground();
    }

    private void RescaleBackgroundBlock(float gameAreaWidth, float backgroundBlockWidth)
    {
        float backgroundBlockScale = gameAreaWidth / backgroundBlockWidth;
        backgroundBlockPrefab.transform.localScale = new Vector2(backgroundBlockScale, 1);
    }

    private void SetInitialPosition(RectTransform gameAreaRectTransform, float backgroundBlockHeight)
    {
        newBlockPosition = new Vector2(0, gameAreaRectTransform.localPosition.y + backgroundBlockHeight / 2);
    }

    private void GenerateInitialBackground()
    {
        for (int _ = 0; _ < backgroundWallHeight; _++)
        {
            AddBackgroundBlockOnTop();
        }
    }

    private void AddBackgroundBlockOnTop()
    {
        GameObject newBackgroundBlock = Instantiate(backgroundBlockPrefab, newBlockPosition, Quaternion.identity, transform);
        backgroundBlocks.Enqueue(newBackgroundBlock);
        newBlockPosition.y += backgroundBlockHeight;
    }
}
