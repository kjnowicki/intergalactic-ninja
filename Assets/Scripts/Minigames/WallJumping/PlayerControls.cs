using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [Header("Time scale during swipe")]
    public float slowMotionValue;

    private Player player;

    private Vector2 swipeStartPosition;

    private Touch touch;

    void Start()
    {
        player = GetComponent<Player>();
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
}
