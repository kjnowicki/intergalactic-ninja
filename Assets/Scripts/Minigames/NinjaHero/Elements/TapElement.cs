using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TapElement : Element
{
    public override async Task SpawnElement(float speed, Spawner spawner, CancellationToken cancellationToken)
    {
        elementRigidbody.AddForce(Vector2.down * speed);
        while (!cancellationToken.IsCancellationRequested && (elementState != ElementState.Spawned))
            await Task.Delay(1);
    }
}
