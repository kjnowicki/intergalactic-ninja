using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour, ICollectable
{
    private BoxCollider2D boxCollider;
    private void Awake()
    {
        boxCollider = FindObjectOfType<BoxCollider2D>();
    }

    public void OnCollected()
    {
        Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        WallJumpingGameManager.Instance.EnableEnviromentMovement();
    }
}
