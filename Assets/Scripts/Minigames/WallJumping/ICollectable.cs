using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    /// <summary>
    /// Use in garbagecollector on trigger enter
    /// </summary>
    void OnCollected();
}
