using UnityEngine;

public class OnWallPlayerState : PlayerState
{
    private readonly int direction;

    public OnWallPlayerState(WallJumpingPlayer player, int direction) : base(player) 
    {
        this.direction = direction;
    }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        if ((swipeStartPosition.x - swipeEndPosition.x) * direction < 0)
        {
            player.Jump(swipeStartPosition, swipeEndPosition);
            player.State = new JumpingPlayerState(player, WallJumpingGameManager.Instance.maximumNumberOfJumps);
        }
    }
}