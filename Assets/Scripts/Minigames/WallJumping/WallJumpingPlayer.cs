using UnityEngine;

public class WallJumpingPlayer : MonoBehaviour
{
    private float forceScale;

    public Rigidbody2D playerRigidbody;
    public bool isTouchingWall { get; set; }

    /// <summary>
    /// Initializes Player parameters
    /// </summary>
    /// <param name="playerForceScale"></param>
    public void Initialize(float playerForceScale, WallJumpingGameManager wallJumpingGameManager)
    {
        forceScale = playerForceScale;
        playerRigidbody = GetComponent<Rigidbody2D>();
        isTouchingWall = false;
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

    /// <summary>
    /// Adds force to players rigid body, resets time scale
    /// </summary>
    public void Jump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        playerRigidbody.AddForce((swipeEndPosition - swipeStartPosition) * forceScale);
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
