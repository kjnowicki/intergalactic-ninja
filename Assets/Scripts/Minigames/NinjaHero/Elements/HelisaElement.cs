using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class HelisaElement : Element
{
    public float spinPartDistance = 0.04f;
    public int fullSwapDelay = 10; //ms
    public int swapFrames = 25;
    public override Task SpawnElement(float speed, Spawner spawner, CancellationToken cancellationToken)
    {
        elementRigidbody.AddForce(Vector2.down * speed);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Method starts unique movement
    /// </summary>
    /// <param name="secondPosition"></param>
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    public async void Spin(Vector2 secondPosition, CancellationToken cancellationToken)
    {
        List<float> targets = new List<float>() { secondPosition.x, spawner.transform.position.x };
        int iterator = 0;
        while (!cancellationToken.IsCancellationRequested && elementState != ElementState.ActionStop)
        {
            await FullSwap(targets[iterator], cancellationToken);
            iterator = (iterator + 1) % 2;
        }
    }

    /// <summary>
    /// One full swap from one patern to other.
    /// </summary>
    /// <param name="targetX"></param>
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    /// <returns></returns>
    private async Task FullSwap(float targetX, CancellationToken cancellationToken)
    {
        try
        {
            for (int i = 0; i < swapFrames; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                float interpolationRatio = i % swapFrames + 1;
                this.transform.position = Vector2.Lerp(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(targetX, this.transform.position.y), spinPartDistance * (interpolationRatio));

                await Task.Delay(fullSwapDelay, cancellationToken);
            }
        }
        catch (OperationCanceledException) { }
    }
}
