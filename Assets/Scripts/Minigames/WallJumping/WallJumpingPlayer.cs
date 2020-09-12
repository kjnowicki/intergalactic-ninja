using UnityEngine;

public class WallJumpingPlayer : MonoBehaviour
{
    private float forceScale;

    public Rigidbody2D playerRigidbody;
    public bool isTouchingWall { get; set; }
    public PlayerState State { get; set; }
    public float Drag { get => playerRigidbody.drag; set => playerRigidbody.drag = value; }

    /// <summary>
    /// Initializes Player parameters
    /// </summary>
    /// <param name="playerForceScale"></param>
    public void Initialize(float playerForceScale, WallJumpingGameManager wallJumpingGameManager)
    {
        forceScale = playerForceScale;
        playerRigidbody = GetComponent<Rigidbody2D>();
        isTouchingWall = false;
        State = new InitialPlayerState(this);
        WallJumpingPlayerControls playerControls = GetComponent<WallJumpingPlayerControls>();
        playerControls.Initialize(this, wallJumpingGameManager);
    }

    /// <summary>
    /// Initializes Player position.
    /// </summary>
    /// <param name="playerInitialPosition"></param>
    public void InitializePosition(Vector2 playerInitialPosition)
    {
        transform.localPosition = playerInitialPosition;
    }

    public void OnJump(Vector2 swipeStartPosition, Vector2 swipeEndPosition) => State.OnJump(swipeStartPosition, swipeEndPosition);

    public void OnTouchTheWall() => State.OnTouchTheWall();

    /// <summary>
    /// Adds force to players rigid body, resets time scale
    /// </summary>
    public void Jump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        Drag = 5;
        playerRigidbody.AddForce((swipeEndPosition - swipeStartPosition) * forceScale);
        State = new JumpingPlayerState(this);
    }

    public void TouchTheWall()
    {
        Drag = 20;
        State = new OnWallPlayerState(this);
    }

    /// <summary>
    /// Obliterates the Player mercilessly.
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
        // GameOver screen in future
    }

}
