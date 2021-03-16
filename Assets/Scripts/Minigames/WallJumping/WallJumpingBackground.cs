using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpingBackground : MonoBehaviour, ICollectable, IAfterCollectable
{
    public void OnAfterCollected()
    {
        Destroy(gameObject);
    }

    public void OnCollected()
    {
        WallJumpingGameManager.Instance.GenerateNewBackgroundOnTop();
    }
}
