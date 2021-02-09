using UnityEngine;

public abstract class BaseWallBlock : MonoBehaviour, ICollectable
{
    public int Direction { get; set; }

    /// <summary>
    /// Abstract method called when the player touches the block.
    /// </summary>
    /// <param name="wallJumpingPlayer"></param>
    public virtual void OnPlayerTouch(WallJumpingPlayer wallJumpingPlayer) => wallJumpingPlayer.State.OnTouchTheWall(Direction, transform.localPosition);

    public void OnCollected()
    {
        if(Direction == 1)
        {
            FindObjectOfType<WallJumpingGameManager>().GenerateNewWallsOnTop();
        }
        Destroy(gameObject);
    }
}
