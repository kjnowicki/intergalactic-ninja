using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TailElement : Element
{

    private SliderElement sliderElementParent;
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

    public override void Score(bool startedScoring)
    {
        if (startedScoring)
        {
            WrongHit();
        }
        else if (!scored)
        {
            CorrectHit();
        }
    }
}
