using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAfterCollectable
{
    /// <summary>
    /// Use in garbagecollector on trigger exit
    /// </summary>
    void OnAfterCollected();
}
