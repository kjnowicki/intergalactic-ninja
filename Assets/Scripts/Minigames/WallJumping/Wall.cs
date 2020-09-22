using System.Collections.Generic;
using UnityEngine;

public class Wall
{
    private readonly Queue<BaseWallBlock> wallBlocks = new Queue<BaseWallBlock>();
    private Vector2 newBlockPosition;
    private readonly float blockHeight;
    private readonly Transform movableObjectsContainer;
    private readonly int direction;

    /// <summary>
    /// Initializes the Wall
    /// </summary>
    /// <param name="intialPosition"></param>
    /// <param name="blockHeight"></param>
    /// <param name="movableObjectsContainer"></param>
    /// <param name="onWallPlayerState"></param>
    public Wall(Vector2 intialPosition, float blockHeight, Transform movableObjectsContainer, int direction)
    {
        newBlockPosition = intialPosition;
        this.blockHeight = blockHeight;
        this.movableObjectsContainer = movableObjectsContainer;
        this.direction = direction;
    }

    /// <summary>
    /// Adds block on the top of current wall.
    /// </summary>
    /// <param name="wallBlockPrefab">
    /// It is used to create new wall object. This must be a Prefab not existing GameObject.
    /// </param>
    public void AddWallBlockOnTop(BaseWallBlock wallBlockPrefab)
    {
        BaseWallBlock newWallBlock = GameObject.Instantiate(wallBlockPrefab, newBlockPosition, Quaternion.identity, movableObjectsContainer);
        newWallBlock.Direction = direction;
        wallBlocks.Enqueue(newWallBlock);
        newBlockPosition.y += blockHeight;
    }
}
