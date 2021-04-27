using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCollider : MonoBehaviour
{
    private new BoxCollider2D collider;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(Camera.main.orthographicSize * 2 * Camera.main.aspect, 1);
    }


}
