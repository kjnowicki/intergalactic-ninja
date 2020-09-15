using UnityEngine;

public class InitialPlayerState : PlayerState
{
    public InitialPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) => player.Jump(swipeStartPosition, swipeEndPosition);

    public override void OnTouchTheWall() => player.TouchTheWall();
}
