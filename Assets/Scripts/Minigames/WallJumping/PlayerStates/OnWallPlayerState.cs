using UnityEngine;

public abstract class OnWallPlayerState : PlayerState
{
    public OnWallPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnTouchTheWall(OnWallPlayerState onWallPlayerState) { } // already touching the wall
}


public class OnLeftWallPlayerState : OnWallPlayerState
{
    public OnLeftWallPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        if (swipeStartPosition.x - swipeEndPosition.x < 0)
        {
            player.Jump(swipeStartPosition, swipeEndPosition);
        }
    }
}

public class OnRightWallPlayerState : OnWallPlayerState
{
    public OnRightWallPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        if (swipeStartPosition.x - swipeEndPosition.x > 0)
        {
            player.Jump(swipeStartPosition, swipeEndPosition);
        }
    }
}