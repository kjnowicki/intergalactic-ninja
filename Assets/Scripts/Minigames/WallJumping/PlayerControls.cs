using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [Header("Time scale during swipe")]
    public float slowMotionValue;

    [Header("Gravity value while being attached to the wall")]
    public float gravityOnWall;
    private float defaultGravity;

    private Player player;

    private Vector2 swipeStartPosition;

    private Touch touch;

    void Start()
    {
        player = GetComponent<Player>();
        defaultGravity = player.playerRigidbody.gravityScale;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Time.timeScale = slowMotionValue;
                    swipeStartPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    Time.timeScale = 1f;
                    player.Jump(swipeStartPosition, touch.position);
                    break;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {            
        if (transform.position.x < (Camera.main.pixelWidth / 2))
        {
            player.playerRigidbody.AddForce(Vector2.left*5);
        }
        else
        {
            player.playerRigidbody.AddForce(Vector2.right*5);
        }
    }
}
