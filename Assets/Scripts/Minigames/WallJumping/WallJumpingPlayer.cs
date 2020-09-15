using UnityEngine;

public class WallJumpingPlayer : MonoBehaviour
{
    private float forceScale;

    public Rigidbody2D playerRigidbody;
    public PlayerState State { get; set; }
    public float Drag { get => playerRigidbody.drag; set => playerRigidbody.drag = value; }

    /// <summary>
    /// Initializes Player parameters
    /// </summary>
    /// <param name="playerForceScale"></param>
    public void Initialize(float playerForceScale)
    {
        forceScale = playerForceScale;
        playerRigidbody = GetComponent<Rigidbody2D>();
        State = new InitialPlayerState(this);
        WallJumpingPlayerControls playerControls = GetComponent<WallJumpingPlayerControls>();
        playerControls.Initialize(this);
    }

    /// <summary>
    /// Initializes Player position.
    /// </summary>
    /// <param name="playerInitialPosition"></param>
    public void InitializePosition(Vector2 playerInitialPosition)
    {
        transform.localPosition = playerInitialPosition;
    }

    /// <summary>
    /// Triggers State's OnJump() method.
    /// </summary>
    /// <param name="swipeStartPosition"></param>
    /// <param name="swipeEndPosition"></param>
    public void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) => State.OnJump(swipeStartPosition, swipeEndPosition);

    /// <summary>
    /// Triggers State's OnTouchTheWall() method.
    /// </summary>
    /// <param name="onWallPlayerState"></param>
    public void OnTouchTheWall(OnWallPlayerState onWallPlayerState) => State.OnTouchTheWall(onWallPlayerState);

    /// <summary>
    /// Adds force to players rigid body, resets time scale
    /// </summary>
    public void Jump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        Drag = 5;
        playerRigidbody.AddForce((swipeEndPosition - swipeStartPosition) * forceScale);
        State = new JumpingPlayerState(this);
    }

    /// <summary>
    /// Makes player touching the wall.
    /// </summary>
    /// <param name="onWallPlayerState"></param>
    public void TouchTheWall(OnWallPlayerState onWallPlayerState)
    {
        Drag = 20;
        playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
        State = onWallPlayerState;
    }

    /// <summary>
    /// Obliterates the Player mercilessly.
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
        // GameOver screen in future
    }

    private void OnTriggerEnter2D(Collider2D collision) => collision.gameObject.GetComponent<BaseWallBlock>()?.OnPlayerTouch(this);
}
