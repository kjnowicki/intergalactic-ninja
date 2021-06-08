using System;
using UnityEngine;

public class JumpingPlayerState : PlayerState
{
    private int remainingNumberOfJumps;
    private double lastVx;
    private bool accelerating;

    public JumpingPlayerState(WallJumpingPlayer player, int maximumNumberOfJumps) : base(player)
    {
        remainingNumberOfJumps = maximumNumberOfJumps - 1; // 1 jump is consumed in OnWallPlayerState
    }

    /// <summary>
    /// Action caused when Player touches the wall while jumping.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="wallBlockPosition"></param>
    public override void OnTouchTheWall(int direction, Vector2 wallBlockPosition)
    {
        player.TouchTheWall(direction, wallBlockPosition);
        player.Direction = direction; 
        player.State = new OnWallPlayerState(player, direction);
    }

    /// <summary>
    /// Performs an air jump it is possible (total number of jumps since last wall contact is lower than limit).  
    /// </summary>
    /// <param name="swipeStartPosition"></param>
    /// <param name="swipeEndPosition"></param>
    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        if (remainingNumberOfJumps > 0)
        {
            player.Jump(swipeStartPosition, swipeEndPosition);
            remainingNumberOfJumps--;
        }
    }

    public override void OnUpdate()
    {
        if (remainingNumberOfJumps == 0)
        {
            double currentVx = player.playerRigidbody.velocity.x;
            double deltaVx = currentVx - lastVx;
            if (deltaVx > 0)
            {
                accelerating = true;
            }

            if (accelerating && Math.Abs(deltaVx) < 0.0001)
            {
                player.Fall();
            }
            lastVx = currentVx;
        }
    }
}