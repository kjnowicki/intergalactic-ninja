using UnityEngine;

public class Player : MonoBehaviour
{
    public float forceScale;

    private Rigidbody2D playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        transform.localPosition = new Vector2(0, 0);
    }

    /// <summary>
    /// Adds force to players rigid body, resets time scale
    /// </summary>
    public void Jump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        playerRigidbody.AddForce((swipeEndPosition - swipeStartPosition) * forceScale);
    }
}
