using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    private const string BACKGROUND_BLOCK_PREFAB_PATH = "Minigames/WallJumping/Prefabs/WallJumpingBackground";
    private const int BACKGROUND_WALL_HEIGHT = 2;

    private Queue<GameObject> backgroundBlocks = new Queue<GameObject>();
    private Vector2 newBlockPosition;
    private GameObject backgroundBlockPrefab;
    private float backgroundBlockHeight;
    private RectTransform gameAreaRectTransform;


    /// <summary>
    /// Initializes BackgroundGenerator.
    /// </summary>
    /// <param name="gameAreaRectTransform"></param>
    public void Initialize(RectTransform gameAreaRectTransform)
    {
        backgroundBlockPrefab = Resources.Load<GameObject>(BACKGROUND_BLOCK_PREFAB_PATH);
        Vector2 backgroundBlockSize = backgroundBlockPrefab.GetComponent<SpriteRenderer>().bounds.size;
        backgroundBlockHeight = backgroundBlockSize.y;
        this.gameAreaRectTransform = gameAreaRectTransform;
        newBlockPosition = new Vector2(0, gameAreaRectTransform.localPosition.y + backgroundBlockHeight / 2);

        RescaleBackgroundBlock(gameAreaRectTransform.sizeDelta.x, backgroundBlockSize.x);
        GenerateInitialBackground();
    }

    public void Restart()
    {
        newBlockPosition = new Vector2(0, gameAreaRectTransform.localPosition.y + backgroundBlockHeight / 2);
        GenerateInitialBackground();
    }

    /// <summary>
    /// Rescales BackgroundBlockPrefab so it matches the screen width.
    /// </summary>
    /// <param name="gameAreaWidth"></param>
    /// <param name="backgroundBlockWidth"></param>
    private void RescaleBackgroundBlock(float gameAreaWidth, float backgroundBlockWidth)
    {
        float backgroundBlockScale = gameAreaWidth / backgroundBlockWidth;
        backgroundBlockPrefab.transform.localScale = new Vector2(backgroundBlockScale, 1);
    }

    /// <summary>
    /// Generates initial background.
    /// </summary>
    private void GenerateInitialBackground()
    {
        for (int _ = 0; _ < BACKGROUND_WALL_HEIGHT; _++)
        {
            AddBackgroundBlockOnTop();
        }
    }

    /// <summary>
    /// Adds single background block on top.
    /// </summary>
    private void AddBackgroundBlockOnTop()
    {
        GameObject newBackgroundBlock = Instantiate(backgroundBlockPrefab, newBlockPosition, Quaternion.identity, transform);
        backgroundBlocks.Enqueue(newBackgroundBlock);
        newBlockPosition.y += backgroundBlockHeight;
    }
}
