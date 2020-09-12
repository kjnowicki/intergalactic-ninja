using UnityEngine;

public abstract class PlayerState
{
    protected WallJumpingPlayer player;

    public PlayerState(WallJumpingPlayer player)
    {
        this.player = player;
    }

    public abstract void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition);
    public abstract void OnTouchTheWall();
}

public class InitialPlayerState : PlayerState
{
    public InitialPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) => player.Jump(swipeStartPosition, swipeEndPosition);

    public override void OnTouchTheWall() => player.TouchTheWall();
}

public class OnWallPlayerState : PlayerState
{
    public OnWallPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) => player.Jump(swipeStartPosition, swipeEndPosition);

    public override void OnTouchTheWall() { } // already touching the wall
}

public class JumpingPlayerState : PlayerState
{
    public JumpingPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) { } // no double jumps

    public override void OnTouchTheWall() => player.TouchTheWall();
}
