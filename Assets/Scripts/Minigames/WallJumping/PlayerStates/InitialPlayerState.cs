using UnityEngine;

public class InitialPlayerState : PlayerState
{
    public InitialPlayerState(WallJumpingPlayer player) : base(player) { }

    /// <summary>
    /// In initial state Player can jump anywhere.
    /// </summary>
    /// <param name="swipeStartPosition"></param>
    /// <param name="swipeEndPosition"></param>
    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) => player.Jump(swipeStartPosition, swipeEndPosition);

    /// <summary>
    /// If player ever touches the wall (for example floor in the future), they should be in new state.
    /// </summary>
    /// <param name="onWallPlayerState"></param>
    public override void OnTouchTheWall(OnWallPlayerState onWallPlayerState) => player.TouchTheWall(onWallPlayerState);
}
