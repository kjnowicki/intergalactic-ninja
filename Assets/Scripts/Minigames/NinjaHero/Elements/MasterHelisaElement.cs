using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class MasterHelisaElement : HelisaElement
{
    public HelisaElement minonElementPrefab;
    private TimeSpan taskTimeOut = new TimeSpan(0, 0, 1);
    private int maxElemnetsToBeClicked;

    private void Start()
    {
        maxElemnetsToBeClicked = NinjaHeroGameManager.Instance.MaxElementsToBeClicked;
    }

    /// <summary>
    /// This gets second spawner and take control over it
    /// </summary>
    /// <returns></returns>
    private Spawner GetMinionSpawner()
    {
        List<Spawner> spawners = NinjaHeroGameManager.Instance.GetSpawners();
        int index = spawners.IndexOf(spawner);

        List<Spawner> neighboursSpawners = new List<Spawner>();
        if (index > 0)
            neighboursSpawners.Add(spawners[index - 1]);
        if (index >= 0 && index < spawners.Count - 1)
            neighboursSpawners.Add(spawners[index + 1]);

        if (neighboursSpawners.Count < 1)
            return null;
        else
            return neighboursSpawners[Random.Range(0, neighboursSpawners.Count - 1)];
    }
    public override async Task SpawnElement(float speed, Spawner spawner, CancellationToken cancellationToken)
    {
        try
        {
            this.spawner = spawner;
            Spawner minionSpawner = GetMinionSpawner();
            using (CancellationTokenSource timeCancellationTokenSource = new CancellationTokenSource(taskTimeOut))
            {
                while ((minionSpawner == null) || this.spawner.suspended || minionSpawner.suspended)
                {
                    await Task.Delay(1, timeCancellationTokenSource.Token);
                }
                timeCancellationTokenSource.Token.ThrowIfCancellationRequested();
            }
            cancellationToken.ThrowIfCancellationRequested();

            minionSpawner.suspended = true;
            await minionSpawner.IsSpawning(cancellationToken);

            HelisaElement minionElement = null;
            if (Random.value < 0.5 && (NinjaHeroGameManager.Instance.ElementsToBeClicked < maxElemnetsToBeClicked))
            {
                NinjaHeroGameManager.Instance.ElementsToBeClicked++;
                minionElement = (HelisaElement)minionSpawner.SpawnElement(minonElementPrefab, cancellationToken).Result;
            }

            cancellationToken.ThrowIfCancellationRequested();
            elementRigidbody.AddForce(Vector2.down * speed);

            while (!cancellationToken.IsCancellationRequested && (elementState != ElementState.Spawned) && ((minionElement == null) || (minionElement.elementState != ElementState.Spawned)))
            { await Task.Delay(1); }

            cancellationToken.ThrowIfCancellationRequested();

            Spin(minionSpawner.transform.position, cancellationToken);
            if (minionElement != null)
            { 
                minionElement.Spin(spawner.transform.position, cancellationToken);
                NinjaHeroGameManager.Instance.ElementsToBeClicked--;
            }

            minionSpawner.suspended = false;
        }
        catch (OperationCanceledException) { }
    }
}



