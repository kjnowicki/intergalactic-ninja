using System.Collections.Generic;
using UnityEngine;

public class Wall
{
    private BaseWallBlock topBlock;
    private Vector2 newBlockPosition;
    private readonly float blockHeight;
    private readonly Transform movableObjectsContainer;
    private readonly int direction;

    private Wall(Vector2 initialPosition, float blockHeight, Transform movableObjectsContainer, int direction)
    {
        newBlockPosition = initialPosition;
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
        var newPosition = new Vector2( topBlock.transform.position.x, topBlock.transform.position.y + blockHeight);
        BaseWallBlock newWallBlock = GameObject.Instantiate(wallBlockPrefab, newPosition, Quaternion.identity, movableObjectsContainer);
        newWallBlock.Direction = direction;
        topBlock = newWallBlock;
    }

    private void AddInitialBlock(BaseWallBlock wallBlockPrefab)
    {
        BaseWallBlock newWallBlock = GameObject.Instantiate(wallBlockPrefab, newBlockPosition, Quaternion.identity, movableObjectsContainer);
        newWallBlock.Direction = direction;
        topBlock = newWallBlock;
    }

    /// <summary>
    /// Initializes wall with first block
    /// </summary>
    /// <param name="initialPosition">Wall initial position</param>
    /// <param name="blockHeight">Block height</param>
    /// <param name="movableObjectsContainer">Paretn container</param>
    /// <param name="direction">Wall direction</param>
    /// <param name="wallBlockPrefab">First block</param>
    /// <returns></returns>
    public static Wall Initialize(Vector2 initialPosition, float blockHeight, Transform movableObjectsContainer, int direction, BaseWallBlock wallBlockPrefab)
    {
        Wall wall = new Wall(initialPosition, blockHeight, movableObjectsContainer, direction);
        wall.AddInitialBlock(wallBlockPrefab);

        return wall;
    } 
}
