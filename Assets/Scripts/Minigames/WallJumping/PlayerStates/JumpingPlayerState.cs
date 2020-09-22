using UnityEngine;

public class JumpingPlayerState : PlayerState
{
    public JumpingPlayerState(WallJumpingPlayer player) : base(player) { }

    /// <summary>
    /// Action caused when player tries to jump while jumping.
    /// </summary>
    /// <param name="swipeStartPosition"></param>
    /// <param name="swipeEndPosition"></param>
    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) => player.Jump(swipeStartPosition, swipeEndPosition); // double jumps will be removed in future

    /// <summary>
    /// Action caused when Player touches the wall while jumping.
    /// </summary>
    /// <param name="direction"></param>
    public override void OnTouchTheWall(int direction) => player.TouchTheWall(direction);
}
