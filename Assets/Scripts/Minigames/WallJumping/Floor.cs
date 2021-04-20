using UnityEngine;

public class Floor : MonoBehaviour, ICollectable
{
    public void OnCollected()
    {
        Destroy(gameObject);
    }
}