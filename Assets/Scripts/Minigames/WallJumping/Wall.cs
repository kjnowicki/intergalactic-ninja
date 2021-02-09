using System.Collections.Generic;
using UnityEngine;

public class Wall
{
    private BaseWallBlock topBlock;
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
    /// <param name="direction"></param>
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
        var newPosition = new Vector2( topBlock.transform.position.x, topBlock.transform.position.y + blockHeight);
        BaseWallBlock newWallBlock = GameObject.Instantiate(wallBlockPrefab, newPosition, Quaternion.identity, movableObjectsContainer);
        newWallBlock.Direction = direction;
        topBlock = newWallBlock;
    }

    public void AddInitialBlock(BaseWallBlock wallBlockPrefab)
    {
        BaseWallBlock newWallBlock = GameObject.Instantiate(wallBlockPrefab, newBlockPosition, Quaternion.identity, movableObjectsContainer);
        newWallBlock.Direction = direction;
        topBlock = newWallBlock;
    }
}
