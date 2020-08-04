using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    private void Start()
    {

    }

    /// <summary>
    /// Move ninja object among tiles
    /// </summary>
    /// <param name="columnTile"></param>
    public void MoveNinja(Vector2 position)
    {
        this.transform.position = new Vector3(position.x, this.transform.position.y, this.transform.position.z);
    }
}
