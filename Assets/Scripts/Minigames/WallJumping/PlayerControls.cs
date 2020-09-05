using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [Header("Time scale during swipe")]
    public float slowMotionValue;

    [Header("Gravity value while being attached to the wall")]
    public float gravityOnWall;

    public WallJumpingGameManager gameManager;

    private float defaultGravity;

    private Player player;

    private Vector2 swipeStartPosition;

    private Touch touch;

    [SerializeField]
    private const string rightWallTag = "RightWall";
    [SerializeField]
    private const string leftWallTag = "LeftWall";
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.enabled = false;
        activateopositeWallColiders(collision);
        player.playerRigidbody.velocity = new Vector2(0f, player.playerRigidbody.velocity.y);
    }

    private void activateopositeWallColiders(Collider2D collision)
    {
        var tag = collision.transform.parent.tag;
        switch (tag)
        {
            case leftWallTag:
                gameManager.ActivateRightWallColiders();
                break;
            case rightWallTag:
                gameManager.ActivateLeftWallColiders();
                break;
            default:
                Debug.Log("Did not hit " + leftWallTag + " or " + rightWallTag);
                break;
        }
    }
}
