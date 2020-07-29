using UnityEngine;

public abstract class BaseWallBlock : MonoBehaviour
{
    /// <summary>
    /// Abstract method called when the player touches the block.
    /// </summary>
    public abstract void OnPlayerTouch();
}
