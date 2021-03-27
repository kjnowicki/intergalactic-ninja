using UnityEngine;

public class JumpingPlayerState : PlayerState
{
    private int remainingNumberOfJumps;

    public JumpingPlayerState(WallJumpingPlayer player, int maximumNumberOfJumps) : base(player)
    {
        remainingNumberOfJumps = maximumNumberOfJumps;
    }

    /// <summary>
    /// Action caused when Player touches the wall while jumping.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="wallBlockPosition"></param>
    public override void OnTouchTheWall(int direction, Vector2 wallBlockPosition)
    {
        player.TouchTheWall(direction, wallBlockPosition);
        player.State = new OnWallPlayerState(player, direction);
    }

    /// <summary>
    /// Performs an air jump it is possible (total number of jumps since last wall contact is lower than limit).  
    /// </summary>
    /// <param name="swipeStartPosition"></param>
    /// <param name="swipeEndPosition"></param>
    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        remainingNumberOfJumps--;
        if (remainingNumberOfJumps > 0)
        {
            player.Jump(swipeStartPosition, swipeEndPosition);
        }
    }
}