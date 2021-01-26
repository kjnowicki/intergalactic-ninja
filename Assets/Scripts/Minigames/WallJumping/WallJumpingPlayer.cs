using System;
using UnityEngine;

public class WallJumpingPlayer : MonoBehaviour
{
    private const float HALF_WALL_WIDTH = 0.5f;
    private float forceScale;
    private float playerToWallOffset;
    private WallJumpingGameManager wallJumpingGameManager;
    private float maximumHeight = 0;

    public Rigidbody2D playerRigidbody;

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
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        playerToWallOffset = HALF_WALL_WIDTH + circleCollider.radius;
        this.wallJumpingGameManager = wallJumpingGameManager;
        State = new InitialPlayerState(this);

        WallJumpingPlayerControls playerControls = GetComponent<WallJumpingPlayerControls>();
        playerControls.Initialize(this);
    }

    private void FixedUpdate()
    {
        float currentPlayerHeight = transform.localPosition.y;
        if (currentPlayerHeight > maximumHeight)
        {
            maximumHeight = currentPlayerHeight;
            wallJumpingGameManager.SetScore(maximumHeight);
        }
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
    /// <param name="direction"></param>
    /// <param name="wallBlockPosition"></param>
    public void TouchTheWall(int direction, Vector2 wallBlockPosition)
    {
        Drag = 20;
        playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
        playerRigidbody.transform.localPosition = new Vector2(wallBlockPosition.x + playerToWallOffset * direction, playerRigidbody.transform.localPosition.y);
        State = new OnWallPlayerState(this, direction);
    }

    /// <summary>
    /// Generates slime effect.
    /// </summary>
    public void GenerateSlimeEffect()
    {
        wallJumpingGameManager.GenerateSlimeEffect();
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
