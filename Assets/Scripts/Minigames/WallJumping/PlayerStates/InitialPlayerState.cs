using UnityEngine;

public class InitialPlayerState : PlayerState
{
    public InitialPlayerState(WallJumpingPlayer player) : base(player) { }

    /// <summary>
    /// In initial state Player can jump anywhere.
    /// </summary>
    /// <param name="swipeStartPosition"></param>
    /// <param name="swipeEndPosition"></param>
    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        player.Jump(swipeStartPosition, swipeEndPosition);
        player.State = new JumpingPlayerState(player, WallJumpingGameManager.Instance.maximumNumberOfJumps);
        WallJumpingGameManager.Instance.EnableEnvironmentMovement();
    }
}
