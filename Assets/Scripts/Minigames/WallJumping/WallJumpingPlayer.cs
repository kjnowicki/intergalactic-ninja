using UnityEngine;

public class WallJumpingPlayer : MonoBehaviour, ICollectable
{
    private const float HALF_WALL_WIDTH = 0.5f;
    private float forceScale;
    private float playerToWallOffset;
    private NinjaAnimator ninjaAnimator;
    private SpriteRenderer spriteRenderer;
    private int direction;
    private float dragInJump;
    private float dragOnWall;
    private float gravityInJump;
    private float gravityOnWall;
    private float gravityOnFall;


    public Rigidbody2D playerRigidbody;

    public PlayerState State { get; set; }

    public float Drag
    {
        get => playerRigidbody.drag;
        set => playerRigidbody.drag = value;
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

    void Update()
    {
        State.OnUpdate();
    }

    /// <summary>
    /// Initializes Player parameters
    /// </summary>
    /// <param name="playerForceScale"></param>
    public void Initialize(float playerForceScale)
    {
        forceScale = playerForceScale;
        WallJumpingGameManager gameManager = WallJumpingGameManager.Instance;
        dragInJump = gameManager.dragInJump;
        dragOnWall = gameManager.dragOnWall;
        gravityInJump = gameManager.gravityInJump;
        gravityOnWall = gameManager.gravityOnWall;
        gravityOnFall = gameManager.gravityOnFall;

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
        Drag = dragInJump;
        playerRigidbody.gravityScale = gravityInJump;
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
        Drag = dragOnWall;
        playerRigidbody.gravityScale = gravityOnWall;
        playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
        playerRigidbody.transform.localPosition = new Vector2(wallBlockPosition.x + playerToWallOffset * direction,
            playerRigidbody.transform.localPosition.y);
        ninjaAnimator.TriggerTouchTheWallAnimation();
    }

    /// <summary>
    /// Makes player fall very fast.
    /// </summary>
    public void Fall()
    {
        playerRigidbody.gravityScale = gravityOnFall;
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