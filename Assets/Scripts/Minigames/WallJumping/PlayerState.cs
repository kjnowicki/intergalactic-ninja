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

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        player.Drag = 5;
        player.Jump(swipeStartPosition, swipeEndPosition);
        player.State = new JumpingPlayerState(player);
    }

    public override void OnTouchTheWall()
    {
        player.Drag = 20;
        player.State = new OnWallPlayerState(player);
    }
}

public class OnWallPlayerState : PlayerState
{
    public OnWallPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        player.Drag = 5;
        player.Jump(swipeStartPosition, swipeEndPosition);
        player.State = new JumpingPlayerState(player);
    }

    public override void OnTouchTheWall() { } // already touching the wall
}

public class JumpingPlayerState : PlayerState
{
    public JumpingPlayerState(WallJumpingPlayer player) : base(player) { }

    public override void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) { } // no double jumps

    public override void OnTouchTheWall()
    {
        player.Drag = 20;
        player.State = new OnWallPlayerState(player);
    }
}
