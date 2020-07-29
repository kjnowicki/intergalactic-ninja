using System;
using UnityEngine;

public abstract class BaseWallBlock : MonoBehaviour
{
    protected const string WALL_BLOCKS_PREFAB_PATH_TEMPLATE = "Minigames/WallJumping/Prefabs/WallBlocks/{0}";

    protected abstract string WallBlockPrefabName { get; }

    public BaseWallBlock WallBlockPrefab
    {
        get
        {
            return Resources.Load<BaseWallBlock>(string.Format(WALL_BLOCKS_PREFAB_PATH_TEMPLATE, WallBlockPrefabName));
        }
    }
}
