using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    List<Element> elementsToSpawnPrefab;
    private float spawnMinInterval;
    private float spawnMaxInterval;
    public bool suspended;
    private bool spawning;
    private int maxElementToBeClicked;

    private void Start()
    {

        #region Infinity loop protection
        spawnMinInterval = spawnMinInterval == 0 ? 1000 : spawnMinInterval;
        spawnMaxInterval = spawnMinInterval == 0 ? 2000 : spawnMaxInterval;
        #endregion

        maxElementToBeClicked = NinjaHeroGameManager.Instance.MaxElementsToBeClicked;
    }
    /// <summary>
    /// Init spawner
    /// </summary>
    /// <param name="spawnMinInterval"></param>
    /// <param name="spawnMaxInterval"></param>
    public void InitSpawner(float spawnMinInterval, float spawnMaxInterval)
    {
        this.spawnMinInterval = spawnMinInterval;
        this.spawnMaxInterval = spawnMaxInterval;
    }
    /// <summary>
    /// Method that start loop with spawninig mechanism
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    /// </summary>
    public async void SpawningElements(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(UnityEngine.Random.Range(spawnMinInterval, spawnMaxInterval)), cancellationToken);
                if ((elementsToSpawnPrefab.Count > 0) && !suspended && !spawning
                    && (NinjaHeroGameManager.Instance.ElementsToBeClicked < maxElementToBeClicked)
                    || cancellationToken.IsCancellationRequested )
                {
                    await SpawnElement(elementsToSpawnPrefab[UnityEngine.Random.Range(0, elementsToSpawnPrefab.Count)], cancellationToken);
                }
            }
        }
        catch (OperationCanceledException) { }
    }
    /// <summary>
    /// Start spawn specify element
    /// </summary>
    /// <param name="elementPrefab"></param>
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    /// <returns></returns>
    public async Task<Element> SpawnElement(Element elementPrefab, CancellationToken cancellationToken)
    {
        Element element;
        try
        {
            NinjaHeroGameManager.Instance.ElementsToBeClicked++;
            cancellationToken.ThrowIfCancellationRequested();
            spawning = true;
            element = Instantiate(elementPrefab, transform.position, transform.rotation, transform);
            element.spawner = this;
            await element.SpawnElement(NinjaHeroGameManager.Instance.speed, this, cancellationToken);
            spawning = false;
            NinjaHeroGameManager.Instance.ElementsToBeClicked--;
        }
        catch (OperationCanceledException)
        {
            element = null;
        }
        return element;
    }
    /// <summary>
    /// Hold thread until spawner is free
    /// </summary>
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    /// <returns></returns>
    public async Task IsSpawning(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested && spawning)
            await Task.Delay(1);
    }

    /// <summary>
    /// Reset spawner flag
    /// </summary>
    public void ResetSpawner()
    {
        suspended = false;
        spawning = false;
    }
}
