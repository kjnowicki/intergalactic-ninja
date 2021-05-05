using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NinjaHeroGarbageCollector : MonoBehaviour
{
    public CancellationToken cancellationToken;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Element>()?.DestroyElement(cancellationToken);
    }
}
