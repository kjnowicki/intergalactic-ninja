using UnityEngine;

public class Matrix : MonoBehaviour
{
    [Header("Matrix parameters")]
    public int width;
    public int height;
    public GameObject backgroundTilePrefab;

    private GameObject[,] _matrixBackgroundTiles;

    private GameObject[] _bubblesPool; // it contains pool of unique bubbles (matrix is filled with those bubbles)

    private readonly string _bubblesAssetPath = "Minigames/Hacking/Prefabs/Bubbles";

    public GameObject[,] MatrixBubbles { get; set; }

    public BaseControlMethod controlMethod;


    //dot => bubble
    void Start()
    {
        _matrixBackgroundTiles = new GameObject[width, height];
        MatrixBubbles = new GameObject[width, height];
        _bubblesPool = Resources.LoadAll<GameObject>(_bubblesAssetPath);
        controlMethod = ControlMethodFactory.GetControlMethod(this);

        SetUp();
    }
    /// <summary>
    /// Method is setting all Matrix with BackgroundTiles (unmovable) and bubbles (movable)
    /// </summary>
    private void SetUp()
    {
        string backgroundTileName = "BackgroundTile ( {0},{1} )";
        string bubbleName = "Bubble ( {0},{1} )";

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 backgroundTilePosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(backgroundTilePrefab, backgroundTilePosition, Quaternion.identity, this.transform) as GameObject;
                backgroundTile.name = string.Format(backgroundTileName, i, j);
                _matrixBackgroundTiles[i, j] = backgroundTile;

                int bubbleToUseIndex = Random.Range(0, _bubblesPool.Length);
                GameObject bubble = Instantiate(_bubblesPool[bubbleToUseIndex], backgroundTilePosition, Quaternion.identity, this.transform) as GameObject;
                bubble.name = string.Format(bubbleName, i, j); ;
                MatrixBubbles[i, j] = bubble;
            }
        }
    }

    private void FixedUpdate()
    {
        controlMethod.ChangePosition();
    }

}

