using UnityEngine;

public class WallJumpingPlayerControls : MonoBehaviour
{
    [Header("Time scale during swipe")]
    public float slowMotionValue;

    private WallJumpingPlayer player;
    private Vector2 swipeStartPosition;

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
            Touch touch = Input.GetTouch(0);
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
