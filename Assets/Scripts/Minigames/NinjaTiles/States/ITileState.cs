using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileState
{
    void SetToGoingToLightenUp();
    void Darken();
    void LightenUp();
}
