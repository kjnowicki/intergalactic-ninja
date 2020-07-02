using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    [Header("Ninja")]
    public float ninja_position_y = 1;

    private void Start()
    {
        SetPosition();
    }
    /// <summary>
    /// Set position of ninja 
    /// </summary>
    public void SetPosition() 
    {
        this.gameObject.transform.position = new Vector2(this.transform.position.x, ninja_position_y);
    }

    /// <summary>
    /// Move ninja object among tiles
    /// </summary>
    /// <param name="columnTile"></param>
    public void MoveNinja( Vector2 position)
    {
        this.transform.position = new Vector2(position.x, ninja_position_y);
    }
}
