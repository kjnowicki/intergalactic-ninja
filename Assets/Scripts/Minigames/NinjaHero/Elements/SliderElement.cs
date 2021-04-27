using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using System.Threading;
using System;
using Random = UnityEngine.Random;

public class SliderElement : Element
{
    public GameObject tailFragmentPrefab;
    public float firstSpritePositionMultiplier = 0.5f;

    private List<TailElement> tailList = new List<TailElement>();
    /// <summary>
    /// Spawns slider element
    /// </summary>
    /// <param name="speed">element speed</param>
    /// <param name="spawner">spawner object</param>
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    /// <returns></returns>
    public override async Task SpawnElement(float speed, Spawner spawner, CancellationToken cancellationToken)
    {
        int minLength = NinjaHeroGameManager.Instance.SliderMinLength;
        int maxLength = NinjaHeroGameManager.Instance.SliderMaxLength;
        try
        {
            float sliderHeight = Height * firstSpritePositionMultiplier; //TrailRenderer will be start in de middle of first element
            elementRigidbody.AddForce(Vector2.down * speed);
            for (int i = 0; i < Random.Range(minLength, maxLength); i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                GameObject tailElement = Instantiate(tailFragmentPrefab, transform.position - Vector3.down * sliderHeight, transform.rotation, this.transform);
                tailList.Add(tailElement.GetComponent<TailElement>());
                tailList[i].SliderElementParent = this;
                sliderHeight += tailList[i].Height;
                await tailList[i].GetComponent<TailElement>().SpawnElement(speed, spawner, cancellationToken);
            }
        }
        catch (OperationCanceledException) { }
    }
    /// <summary>
    /// Dastroys object
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    /// </summary>
    public override async void DestroyElement(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested && tailList.Last() != null)
                await Task.Delay(1, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            Destroy(gameObject);
        }
        catch (OperationCanceledException) { }
    }
}
