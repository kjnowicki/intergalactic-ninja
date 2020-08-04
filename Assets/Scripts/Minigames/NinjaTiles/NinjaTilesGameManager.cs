using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static ColumnTile;

public class NinjaTilesGameManager : MonoBehaviour
{
    #region singleton
    private static NinjaTilesGameManager instance = null;

    public static NinjaTilesGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NinjaTilesGameManager>();

                if (instance == null)
                {
                    instance = new NinjaTilesGameManager();
                }
            }
            return instance;
        }
    }
    #endregion

    [Header("Main objects")]
    public GameObject tilesContainerPrefab;
    public GameObject ninjaPrefab;
    public GameObject mainCamera;
    public GameObject gameObjectsContainer;

    private GameObject tilesContainer;
    private GameObject ninja;

    private const float UNCHANGED_SCALE_FACTOR = 1;

    void Start()
    {
        InitializeData();
    }

    /// <summary>
    /// Initializes data on scene startup
    /// </summary>
    private void InitializeData()
    {
        SetData();
        SetGameObjectsContainerScale();
    }

    private void SetData()
    {
        tilesContainer = Instantiate(tilesContainerPrefab);
        tilesContainer.transform.SetParent(gameObjectsContainer.transform);
        ninja = Instantiate(ninjaPrefab);
        ninja.transform.parent = gameObjectsContainer.transform;
    }

    /// <summary>
    /// Set objects' width on the scene based on the device resolution
    /// </summary>
    private void SetGameObjectsContainerScale()
    {
        float widthScaleForTilesContainer = mainCamera.GetComponent<Camera>().orthographicSize * 2 / tilesContainer.GetComponentInChildren<SpriteRenderer>().size.x;
        Vector2 scale = new Vector2(widthScaleForTilesContainer * Screen.width / Screen.height, UNCHANGED_SCALE_FACTOR);
        gameObjectsContainer.transform.localScale = new Vector3(scale.x, scale.y, UNCHANGED_SCALE_FACTOR);
        ninja.transform.localScale = new Vector3(UNCHANGED_SCALE_FACTOR, scale.x, UNCHANGED_SCALE_FACTOR);
    }

    /// <summary>
    /// Move ninja to column
    /// </summary>
    /// <param name="columnTile"></param>
    public void MoveNinjaToTile(GameObject columnTile)
    {
        ninja.GetComponent<Ninja>().MoveNinja(columnTile.transform.position);
    }
}
