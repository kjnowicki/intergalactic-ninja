using UnityEngine;

public class Player : MonoBehaviour
{
    public float forceScale;

    public Rigidbody2D playerRigidbody;
    public bool isTouchingWall { get; set; }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        isTouchingWall = false;
    }

    public void Initialize(Vector2 initialPosition)
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        transform.localPosition = initialPosition;
        PlayerControls playerControls = GetComponent<PlayerControls>();
        playerControls.Initialize(this);
    }

    /// <summary>
    /// Adds force to players rigid body, resets time scale
    /// </summary>
    public void Jump(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
    {
        playerRigidbody.AddForce((swipeEndPosition - swipeStartPosition) * forceScale);
    }

}
