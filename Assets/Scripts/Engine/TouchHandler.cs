using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class TouchHandler
{
    /// <summary>
    /// Performs OnTouch if clicable object was touched
    /// </summary>
    public static void HandleTouches()
    {
        Camera mainCam = Camera.main;
        foreach (Touch touch in Input.touches)
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(touch.position), Vector2.zero);
            Clickable target = hit.transform?.gameObject.GetComponent<Clickable>();

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    target?.OnTouchDown();
                    break;
                case TouchPhase.Ended:
                    target?.OnTouchUp();
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    target?.OnTouchDrag();
                    break;
                default:
                    target?.OnTouch();
                    break;
            }
        }
    }
}

