using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Queue<BaseWallBlock> wallBlocks = new Queue<BaseWallBlock>();
    private Vector2 newBlockPosition;
    private float blockHeight;
    private Transform movableObjectsContainer;

    /// <summary>
    /// Initializes the Wall
    /// </summary>
    /// <param name="intialPosition"></param>
    /// <param name="blockHeight"></param>
    public void Initialize(Vector2 intialPosition, float blockHeight, Transform movableObjectsContainer)
    {
        newBlockPosition = intialPosition;
        this.blockHeight = blockHeight;
        movableObjectsContainer = movableObjectsContainer;
    }

    /// <summary>
    /// Adds block on the top of current wall.
    /// </summary>
    /// <param name="wallBlockPrefab">
    /// It is used to create new wall object. This must be a Prefab not existing GameObject.
    /// </param>
    public void AddWallBlockOnTop(BaseWallBlock wallBlockPrefab)
    {
        BaseWallBlock newWallBlock = Instantiate(wallBlockPrefab, newBlockPosition, Quaternion.identity, movableObjectsContainer);
        wallBlocks.Enqueue(newWallBlock);
        newBlockPosition.y += blockHeight;
    }
}
