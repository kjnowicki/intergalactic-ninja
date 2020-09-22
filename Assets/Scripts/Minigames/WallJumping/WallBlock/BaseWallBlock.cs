using UnityEngine;

public abstract class BaseWallBlock : MonoBehaviour
{
    public int Direction { get; set; }

    /// <summary>
    /// Abstract method called when the player touches the block.
    /// </summary>
    public virtual void OnPlayerTouch(WallJumpingPlayer wallJumpingPlayer) => wallJumpingPlayer.State.OnTouchTheWall(Direction);
}
