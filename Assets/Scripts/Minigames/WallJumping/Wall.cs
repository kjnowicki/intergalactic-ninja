using System.Collections.Generic;
using UnityEngine;

public class Wall
{
    private readonly Queue<BaseWallBlock> wallBlocks = new Queue<BaseWallBlock>();
    private Vector2 newBlockPosition;
    private float blockHeight;
    private Transform movableObjectsContainer;
    private OnWallPlayerState onWallPlayerState;

    /// <summary>
    /// Initializes the Wall
    /// </summary>
    /// <param name="intialPosition"></param>
    /// <param name="blockHeight"></param>
    public Wall(Vector2 intialPosition, float blockHeight, Transform movableObjectsContainer, OnWallPlayerState onWallPlayerState)
    {
        newBlockPosition = intialPosition;
        this.blockHeight = blockHeight;
        this.movableObjectsContainer = movableObjectsContainer;
        this.onWallPlayerState = onWallPlayerState;
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
        newWallBlock.WallSideState = onWallPlayerState;
        wallBlocks.Enqueue(newWallBlock);
        newBlockPosition.y += blockHeight;
    }
}
