using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TouchHandler
{
    /// <summary>
    /// Performs OnTouch if clicable object was touchedS
    /// </summary>
    public static void HandleTouches()
    {
        foreach (Touch touch in Input.touches)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit))
            {
                if (hit.transform.gameObject.GetComponent<Collider2D>() != null)
                {
                    hit.transform.gameObject.GetComponent<Clicable>()?.OnTouch();
                }
            }
        }
    }

    /// <summary>
    /// Performs OnTouchDown if clicable object was touched
    /// </summary>
    public static void HandleTouchesDown()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit))
                {
                    if (hit.transform.gameObject.GetComponent<Collider2D>() != null)
                    {
                        hit.transform.gameObject.GetComponent<Clicable>()?.OnTouchDown();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Performs OnTouchUp if clicable object was touched
    /// </summary>
    public static void HandleTouchesUp()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit))
                {
                    if (hit.transform.gameObject.GetComponent<Collider2D>() != null)
                    {
                        hit.transform.gameObject.GetComponent<Clicable>()?.OnTouchUp();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Performs OnTouchDrag if clicable object was draged
    /// </summary>
    /// <param name="onClick"></param>
    public static void HandleTouchesDrag()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit))
                {
                    if (hit.transform.gameObject.GetComponent<Collider2D>() != null)
                    {
                        hit.transform.gameObject.GetComponent<Clicable>()?.OnTouchDrag();
                    }
                }
            }
        }
    }
}

