using UnityEngine;

public abstract class PlayerState
{
    protected WallJumpingPlayer player;

    public PlayerState(WallJumpingPlayer player)
    {
        this.player = player;
    }

    public abstract void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition);

    public abstract void OnTouchTheWall(int direction, Vector2 wallBlockPosition);
}
