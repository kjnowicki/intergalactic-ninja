using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class PaternTilesContainerManager : MonoBehaviour
{
    [Header("Main objects")]
    private int width; //number of patern tiles
    private readonly int height = 7;
    [SerializeField] private float clicableAreaPositonY;
    [SerializeField] private float spawnerPositonY;

    public GameObject paternTilePrefab;

    private List<PaternTile> paternTiles;
    private const float GAMEOBJECTS_SCALE_WIDTH = 8f;
    private const float GAMEOBJECTS_SCALE_HEIGHT = 1;

    /// <summary>
    /// Initializes data on scene startup
    /// </summary>
    public void InitializeData(int width, float spawnMinInterval, float spawnMaxInterval)
    {
        this.width = width;
        paternTiles = new List<PaternTile>();
        SetContainerSize();
        InitPaternTile(spawnMinInterval, spawnMaxInterval);
        SetContainerScale();
    }

    /// <summary>
    /// Adjust image, size and creates clicable areas. 
    /// </summary>
    private void SetContainerSize()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(width, height);
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(width, height);
    }
    /// <summary>
    /// Init patern tiles.
    /// </summary>
    private void InitPaternTile(float spawnMinInterval, float spawnMaxInterval)
    {
        for (int i = 1; i <= width; i++)
        {
            paternTiles.Add(Instantiate(paternTilePrefab, transform.position, transform.rotation, transform).GetComponent<PaternTile>());
            paternTiles.Last().InitPaternTile(new Vector2(transform.position.x, clicableAreaPositonY), new Vector2(transform.position.x, spawnerPositonY), i, spawnMinInterval, spawnMaxInterval);
        }
    }

    /// <summary>
    /// Set objects' width on the scene based on the device resolution
    /// </summary>
    private void SetContainerScale()
    {
        float gameObjectsScaleWidth = GAMEOBJECTS_SCALE_WIDTH;
        gameObjectsScaleWidth /= width;
        Vector2 scale = new Vector2(gameObjectsScaleWidth * Screen.width / Screen.height, GAMEOBJECTS_SCALE_HEIGHT);
        this.transform.localScale *= scale;
    }

    /// <summary>
    /// Get spawner list
    /// </summary>
    /// <returns></returns>
    public List<Spawner> GetSpawners()
    {
        return paternTiles.Select(x => x.Spawner).ToList();
    }
    /// <summary>
    /// Start swpawn elements used for setup and reset
    /// </summary>
    /// <param name="cancellationToken">cancellation Token for async thread</param>
    public void StartSpawn(CancellationToken cancellationToken)
    {
        foreach (Spawner spawner in GetSpawners())
        {
            spawner.ResetSpawner();
            spawner.SpawningElements(cancellationToken);
        }
    }
}
