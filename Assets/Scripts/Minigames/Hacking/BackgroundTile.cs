using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    public GameObject[] dots;
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        int dotToUseIndex = Random.Range(0, dots.Length);
        GameObject dot = Instantiate(dots[dotToUseIndex], transform.position, Quaternion.identity, this.transform) as GameObject;
        dot.name = this.gameObject.name;
    }
}
