using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializePosition(Vector2 garbageCollectorInitialPosition)
    {
        transform.localPosition = garbageCollectorInitialPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Player>()?.Die(); // shorter for if (collision.gameObject.GetComponent<Player>() != null) collision.gameObject.GetComponent<Player>().Die()
    }
}
