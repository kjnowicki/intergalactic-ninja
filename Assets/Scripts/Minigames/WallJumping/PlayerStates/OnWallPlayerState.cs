using UnityEngine;

public class OnWallPlayerState : PlayerState
{
    private readonly int direction;

    public OnWallPlayerState(WallJumpingPlayer player, int direction) : base(player) 
    {
        this.direction = direction;
    }

    /// <summary>
    /// When player touches the wall while being on the wall nothing should happen.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="wallBlockPosition"></param>
    public override void OnTouchTheWall(int direction, Vector2 wallBlockPosition) { } // already touching the wall

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        if ((swipeStartPosition.x - swipeEndPosition.x) * direction < 0)
        {
            player.Jump(swipeStartPosition, swipeEndPosition);
        }
    }
}