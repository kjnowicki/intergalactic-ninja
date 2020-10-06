using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clicable : MonoBehaviour
{
    public virtual void OnTouch() { }
    public virtual void OnTouchDown() { }
    public virtual void OnTouchUp() { }
    public virtual void OnTouchDrag() { }
}
