using UnityEngine;

public class JumpingPlayerState : PlayerState
{
    public JumpingPlayerState(WallJumpingPlayer player) : base(player) { }

    /// <summary>
    /// Action caused when Player touches the wall while jumping.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="wallBlockPosition"></param>
    public override void OnTouchTheWall(int direction, Vector2 wallBlockPosition) => player.TouchTheWall(direction, wallBlockPosition);
}
