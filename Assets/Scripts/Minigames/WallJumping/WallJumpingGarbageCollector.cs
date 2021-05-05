using UnityEngine;

public class WallJumpingGarbageCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<ICollectable>()?.OnCollected();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<IAfterCollectable>()?.OnAfterCollected();
    }
}
