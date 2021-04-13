using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    /// <summary>
    /// Move ninja object among tiles
    /// </summary>
    /// <param name="columnTile"></param>
    public void MoveNinja(Vector2 position)
    {
        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(position.x, currentPosition.y, currentPosition.z);
    }
}
