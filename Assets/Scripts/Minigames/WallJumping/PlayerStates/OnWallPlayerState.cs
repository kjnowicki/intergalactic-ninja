using UnityEngine;

public abstract class OnWallPlayerState : PlayerState
{
    public OnWallPlayerState(WallJumpingPlayer player) : base(player) { }

    /// <summary>
    /// When player touches the wall while being on the wall nothing should happen.
    /// </summary>
    /// <param name="onWallPlayerState"></param>
    public override void OnTouchTheWall(OnWallPlayerState onWallPlayerState) { } // already touching the wall
}


public class OnLeftWallPlayerState : OnWallPlayerState
{
    public OnLeftWallPlayerState(WallJumpingPlayer player) : base(player) { }

    /// <summary>
    /// Limits angle of the jump so Player can only jump in the right direction.
    /// </summary>
    /// <param name="swipeStartPosition"></param>
    /// <param name="swipeEndPosition"></param>
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

    /// <summary>
    /// Limits angle of the jump so Player can only jump in the left direction.
    /// </summary>
    /// <param name="swipeStartPosition"></param>
    /// <param name="swipeEndPosition"></param>
    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        if (swipeStartPosition.x - swipeEndPosition.x > 0)
        {
            player.Jump(swipeStartPosition, swipeEndPosition);
        }
    }
}