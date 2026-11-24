using UnityEngine;

public class WallJumpingPlayer : MonoBehaviour, ICollectable
{
    private const float HALF_WALL_WIDTH = 0.5f;
    private float forceScale;
    private float playerToWallOffset;
    private NinjaAnimator ninjaAnimator;
    private SpriteRenderer spriteRenderer;
    private int direction;

    public Rigidbody2D playerRigidbody;

    public PlayerState State { get; set; }

    public float Drag
    {
        get => playerRigidbody.linearDamping;
        set => playerRigidbody.linearDamping = value;
    }

    public int Direction
    {
        get => direction;
        set
        {
            direction = value;
            spriteRenderer.flipX = direction == 1;
        }
    }

    /// <summary>
    /// Initializes Player parameters
    /// </summary>
    /// <param name="playerForceScale"></param>
    public void Initialize(float playerForceScale)
    {
        forceScale = playerForceScale;
        playerRigidbody = GetComponent<Rigidbody2D>();
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerToWallOffset = HALF_WALL_WIDTH + capsuleCollider.size.x / 2;
        State = new InitialPlayerState(this);
        ninjaAnimator = GetComponent<NinjaAnimator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        WallJumpingPlayerControls playerControls = GetComponent<WallJumpingPlayerControls>();
        playerControls.Initialize(this);
    }

    /// <summary>
    /// Adds force to players rigid body, resets time scale
    /// </summary>
    public void Jump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        Drag = 5;
        var xPositionDelta = swipeEndPosition.x - swipeStartPosition.x;
        if (xPositionDelta > 0)
        {
            Direction = -1;
        }
        else
        {
            Direction = 1;
        }

        playerRigidbody.AddForce((swipeEndPosition - swipeStartPosition) * forceScale);
        ninjaAnimator.TriggerJumpAnimation();
    }

    /// <summary>
    /// Makes player touching the wall.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="wallBlockPosition"></param>
    public void TouchTheWall(int direction, Vector2 wallBlockPosition)
    {
        Drag = 20;
        playerRigidbody.linearVelocity = new Vector2(0f, playerRigidbody.linearVelocity.y);
        playerRigidbody.transform.localPosition = new Vector2(wallBlockPosition.x + playerToWallOffset * direction,
            playerRigidbody.transform.localPosition.y);
        ninjaAnimator.TriggerTouchTheWallAnimation();
    }

    /// <summary>
    /// Generates slime effect.
    /// </summary>
    public void GenerateSlimeEffect()
    {
        WallJumpingGameManager.Instance.GenerateSlimeEffect();
    }


    /// <summary>
    /// Obliterates the Player mercilessly.
    /// </summary>
    public void OnCollected()
    {
        Destroy(gameObject);
        WallJumpingGameManager.Instance.OnGameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<BaseWallBlock>()?.OnPlayerTouch(this);
    }
}
