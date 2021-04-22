using UnityEngine;

public class DestroyCollectable : MonoBehaviour, ICollectable
{
    public void OnCollected()
    {
        Destroy(gameObject);
    }
}