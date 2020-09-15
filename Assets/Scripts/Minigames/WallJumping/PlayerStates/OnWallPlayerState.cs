using UnityEngine;

public abstract class OnWallPlayerState : PlayerState
{
    public OnWallPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) => player.Jump(swipeStartPosition, swipeEndPosition);

    public override void OnTouchTheWall() { } // already touching the wall
}
