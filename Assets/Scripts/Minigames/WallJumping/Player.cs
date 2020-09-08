using UnityEngine;

public class Player : MonoBehaviour
{
    private float forceScale;

    private Rigidbody2D playerRigidbody;

    /// <summary>
    /// Initializes Player parameters
    /// </summary>
    /// <param name="playerForceScale"></param>
    /// <param name="playerHeightToDie"></param>
    public void Initialize(float playerForceScale, float playerHeightToDie)
    {
        this.forceScale = playerForceScale;
    }

    /// <summary>
    /// Initializes Player position.
    /// </summary>
    /// <param name="playerInitialPosition"></param>
    public void InitializePosition(Vector2 playerInitialPosition)
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
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
