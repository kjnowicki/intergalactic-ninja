using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    /// <summary>
    /// Initializes GarbageCollector position.
    /// </summary>
    /// <param name="garbageCollectorInitialPosition"></param>
    public void InitializePosition(Vector2 garbageCollectorInitialPosition)
    {
        transform.localPosition = garbageCollectorInitialPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<ICollectable>()?.OnCollected();
    }
}
