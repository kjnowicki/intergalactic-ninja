using UnityEngine;

public abstract class PlayerState
{
    protected WallJumpingPlayer player;

    public PlayerState(WallJumpingPlayer player)
    {
        this.player = player;
    }

    public virtual void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) { }

    public virtual void OnTouchTheWall(int direction, Vector2 wallBlockPosition) { }
    
    public virtual void OnUpdate() { }
}
