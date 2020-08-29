using UnityEngine;

public class Player : MonoBehaviour
{
    public float forceScale;
    public float playerHeightToDie;

    private Rigidbody2D playerRigidbody;

    private void Update()
    {
        if (transform.localPosition.y < playerHeightToDie)
        {
            Die();
        }
    }

    public void Initialize(Vector2 playerInitialPosition)
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
    private void Die()
    {
        Destroy(gameObject);
        // GameOver screen in future
    }
}
