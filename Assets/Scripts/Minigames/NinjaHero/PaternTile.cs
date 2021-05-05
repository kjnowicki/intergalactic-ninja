using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PaternTile : MonoBehaviour
{
    public GameObject clickableAreaPrefab;
    public GameObject spawnerPrefab;
    public ClickableArea ClickableArea { get; private set; }
    public Spawner Spawner { get; private set; }

    private const string PATERN_TILE_NAME = "Patern Tile {0}";
    private const string CLICKABLE_AREA_NAME = "Clickable Area {0}";
    private const string SPAWNER_NAME = "Spawner {0}";

    /// <summary>
    /// Setting the entire pattern column
    /// </summary>
    /// <param name="clickableAreaPositon"></param>
    /// <param name="spawnerPosition"></param>
    /// <param name="order"></param>
    public void InitPaternTile(Vector2 clickableAreaPositon, Vector2 spawnerPosition, int order, float spawnMinInterval, float spawnMaxInterval)
    {
        this.name = string.Format(PATERN_TILE_NAME, order);

        InitSpawner(spawnerPosition, order, spawnMinInterval, spawnMaxInterval);
        InitClickableArea(clickableAreaPositon, order);
    }
    /// <summary>
    /// Init spawner
    /// </summary>
    /// <param name="spawnerPosition"></param>
    /// <param name="order"></param>
    /// <param name="spawnMinInterval"></param>
    /// <param name="spawnMaxInterval"></param>
    private void InitSpawner(Vector2 spawnerPosition, int order, float spawnMinInterval, float spawnMaxInterval)
    {
        this.Spawner = Instantiate(spawnerPrefab, spawnerPosition, transform.rotation, this.transform).GetComponent<Spawner>();
        Spawner.name = string.Format(SPAWNER_NAME, order);
        Spawner.InitSpawner(spawnMinInterval, spawnMaxInterval);
    }
    /// <summary>
    /// Init Clickable area
    /// </summary>
    /// <param name="clickableAreaPositon"></param>
    /// <param name="order"></param>
    private void InitClickableArea(Vector2 clickableAreaPositon, int order)
    {
        this.ClickableArea = Instantiate(clickableAreaPrefab, clickableAreaPositon, transform.rotation, this.transform).GetComponent<ClickableArea>();
        ClickableArea.name = string.Format(CLICKABLE_AREA_NAME, order);
    }
}
