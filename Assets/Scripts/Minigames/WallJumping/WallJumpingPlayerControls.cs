using System.Collections;
using UnityEngine;

public class WallJumpingPlayerControls : MonoBehaviour
{
    [Header("Time scale during swipe")]
    public float slowMotionValue;

    [Header("Gravity value while being attached to the wall")]
    public float gravityOnWall;

    private WallJumpingPlayer player;
    private Vector2 swipeStartPosition;
    private Touch touch;

    [SerializeField]
    private const string rightWallTag = "RightWall";
    [SerializeField]
    private const string leftWallTag = "LeftWall";

    /// <summary>
    /// Initializes PlayerControls
    /// </summary>
    /// <param name="player"></param>
    public void Initialize(WallJumpingPlayer player)
    {
        this.player = player;
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
                    player.State.OnJump(swipeStartPosition, touch.position);
                    break;
            }
        }
    }
}
