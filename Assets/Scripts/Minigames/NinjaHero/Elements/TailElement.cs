using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TailElement : Element
{

    private SliderElement sliderElementParent;
    private bool scored = false;
    public SliderElement SliderElementParent { get => sliderElementParent; set => sliderElementParent = value; }

    /// <summary>
    /// Spawns tail element
    /// </summary>
    /// <param name="speed">element speed</param>
    /// <param name="spawner">spawner object</param>
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    /// <returns></returns>
    public override async Task SpawnElement(float speed, Spawner spawner, CancellationToken cancellationToken)
    {
        elementRigidbody.AddForce(Vector2.down * speed);
        while (!cancellationToken.IsCancellationRequested && elementState == ElementState.NotSpawned)
        {
            await Task.Delay(1);
        }
    }
    /// <summary>
    /// Decides if element was hit proprely or not
    /// </summary>
    /// <param name="tap">tap</param>
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    /// <returns></returns>
    public async override Task HitElement(bool tap, CancellationToken cancellationToken)
    {
        if (!cancellationToken.IsCancellationRequested && !scored)
        {
            if ((SliderElementParent.elementState == ElementState.CorrectHit) && !tap)
            {
                elementState = ElementState.CorrectHit;
                CorrectHit();
            }
            else
            {
                SliderElementParent.elementState = ElementState.WrongHit;
                elementState = ElementState.WrongHit;
            }
            scored = true;
            await Task.Yield();
        }
    }
}
