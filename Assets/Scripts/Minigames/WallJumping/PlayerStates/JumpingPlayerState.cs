using UnityEngine;

public class JumpingPlayerState : PlayerState
{
    public JumpingPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) => player.Jump(swipeStartPosition, swipeEndPosition); // double jumps will be removed in future

    public override void OnTouchTheWall(OnWallPlayerState onWallPlayerState) => player.TouchTheWall(onWallPlayerState);
}
