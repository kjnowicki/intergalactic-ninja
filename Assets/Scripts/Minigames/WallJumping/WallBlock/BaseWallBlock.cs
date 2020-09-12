using UnityEngine;

public abstract class BaseWallBlock : MonoBehaviour
{
    public WallSide WallSide { get; set; }

    /// <summary>
    /// Abstract method called when the player touches the block.
    /// </summary>
    public virtual void OnPlayerTouch(WallJumpingPlayer wallJumpingPlayer) => wallJumpingPlayer.OnTouchTheWall();
}

public enum WallSide
{
    Left,
    Right
}
