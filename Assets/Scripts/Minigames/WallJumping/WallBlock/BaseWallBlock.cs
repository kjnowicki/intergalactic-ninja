using UnityEngine;

public abstract class BaseWallBlock : MonoBehaviour, ICollectable
{
    private int Direction { get; set; }

    public virtual void Initialize(int direction)
    {
        Direction = direction;
        if (Direction == 1)
        {
            FlipSprite();
        }
    }

    /// <summary>
    /// Abstract method called when the player touches the block.
    /// </summary>
    /// <param name="wallJumpingPlayer"></param>
    public virtual void OnPlayerTouch(WallJumpingPlayer wallJumpingPlayer)
    {
        wallJumpingPlayer.State.OnTouchTheWall(Direction, transform.localPosition);
    }

    public void OnCollected()
    {
        if(Direction == 1)
        {
            WallJumpingGameManager.Instance.GenerateNewWallsOnTop();
        }
        Destroy(gameObject);
    }

    protected virtual void FlipSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
    }
}
