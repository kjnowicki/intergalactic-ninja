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
    public GameObject gameObjectsContainerPrefab;
    public GameObject ninjaPrefab;

    
    private GameObject _gameObjectsContainer;
    private GameObject _ninja;
    private Vector2 _scale;

    private const float _GAMEOBJECTS_SCALE_WIDTH = 8f / 5f;
    private const float _GAMEOBJECTS_SCALE_HEIGHT = 1;

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
        _gameObjectsContainer = Instantiate(gameObjectsContainerPrefab);
        _ninja = Instantiate(ninjaPrefab);
    }
    /// <summary>
    /// Set objects' width on the scene based on the device resolution
    /// </summary>
    private void SetGameObjectsContainerScale()
    {
        _scale = new Vector2(_GAMEOBJECTS_SCALE_WIDTH * Screen.width / Screen.height, _GAMEOBJECTS_SCALE_HEIGHT);
        _gameObjectsContainer.transform.localScale *= _scale;
    }
    /// <summary>
    /// Move ninja to column
    /// </summary>
    /// <param name="columnTile"></param>
    public void MoveNinjaToTile(GameObject columnTile)
    {
        _ninja.GetComponent<Ninja>().MoveNinja(columnTile.transform.position);
    }

}
