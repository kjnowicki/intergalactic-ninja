using System;
using UnityEngine;

public class JumpingPlayerState : PlayerState
{
    private readonly double vxFallingThreshold;
    
    private int remainingNumberOfJumps;
    private double lastVx;
    private bool accelerating;

    public JumpingPlayerState(WallJumpingPlayer player) : base(player)
    {
        WallJumpingGameManager gameManager = WallJumpingGameManager.Instance;

        vxFallingThreshold = gameManager.vxFallingThreshold;
        // 1st jump is consumed in OnWallPlayerState or InitialState
        remainingNumberOfJumps = gameManager.maximumNumberOfJumps - 1;
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
            
            if (accelerating && Math.Abs(deltaVx) < vxFallingThreshold)
            {
                player.Fall();
            }
            lastVx = currentVx;
        }
    }
}