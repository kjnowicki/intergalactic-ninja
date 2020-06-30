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

    [Header("Ninja")]
    public float ninja_position_y = 1;
    
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

        Transform ninjaPosition = this.gameObject.transform;
        ninjaPosition.position = new Vector2(ninjaPosition.position.x, ninja_position_y);
        _ninja = Instantiate(ninjaPrefab, ninjaPosition);
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
    /// Move ninja object among tiles
    /// </summary>
    /// <param name="columnTile"></param>
    public void MoveNinjaToTile(GameObject columnTile) 
    {
        _ninja.transform.position = new Vector2 (columnTile.transform.position.x , ninja_position_y);
    }
}
