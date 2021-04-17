using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    private RectTransform gameAreaRectTransform;
    private WallGenerator wallGenerator;
    private MovableObjectsContainer movableObjectsContainer;
    private WallJumpingPlayer player;
    private BackgroundGenerator backgroundGenerator;
    [SerializeField] private CameraRubberBand cameraRubberBand;

    [SerializeField] private GameObject floor;

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="wallJumpingGameManager"></param>
    /// <param name="heightRatio"></param>
    /// <param name="wallHeightAdditional"></param>
    /// <param name="garbageCollectorHeight"></param>
    /// <param name="slimeEffectDuration"></param>
    public void Initialize(WallJumpingGameManager wallJumpingGameManager, float heightRatio, int wallHeightAdditional, float garbageCollectorHeight, float slimeEffectDuration)
    {
        gameAreaRectTransform = GetComponent<RectTransform>();
        movableObjectsContainer = GetComponentInChildren<MovableObjectsContainer>();
        backgroundGenerator = GetComponentInChildren<BackgroundGenerator>();
        
        InitializeWallGenerator(wallHeightAdditional);
        InitializeBackgroundGenerator();
        InitializeSlimeGenerator(slimeEffectDuration);
        InitializeRubberBandAreaSize(wallJumpingGameManager);
        InitializeFloor();
    }


    /// <summary>
    /// Initializes Player.
    /// </summary>
    public void InitializePlayer(float playerForceScale, WallJumpingGameManager wallJumpingGameManager)
    {
        movableObjectsContainer.InitializePlayer(playerForceScale, wallJumpingGameManager);
    }
    
    /// <summary>
    /// Destroys objects from previous game.
    /// </summary>
    public void DestroyObjectsFromPreviousGame()
    {
        List<Transform> children = new List<Transform>(movableObjectsContainer.GetComponentsInChildren<Transform>());
        children.Remove(movableObjectsContainer.transform);
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }
    }
    
    /// <summary>
    /// Restarts GameArea part of the game.
    /// </summary>
    public void Restart()
    {
        backgroundGenerator.Restart();
        wallGenerator.Restart();
        InitializeFloor();
    }

    /// <summary>
    /// Initializes WallGenerator.
    /// </summary>
    /// <param name="wallHeightAdditional"></param>
    private void InitializeWallGenerator(int wallHeightAdditional)
    {
        wallGenerator = GetComponentInChildren<WallGenerator>();
        wallGenerator.Initialize(wallHeightAdditional, gameAreaRectTransform);
    }

    /// <summary>
    /// Initializes BackgroundGenerator.
    /// </summary>
    private void InitializeBackgroundGenerator()
    {
        backgroundGenerator.Initialize(gameAreaRectTransform);
    }

    /// <summary>
    /// Initializes SlimeGenerator.
    /// </summary>
    /// <param name="slimeEffectDuration"></param>
    private void InitializeSlimeGenerator(float slimeEffectDuration)
    {
        SlimeGenerator slimeGenerator = FindObjectOfType<SlimeGenerator>();
        slimeGenerator.Initialize(gameAreaRectTransform, slimeEffectDuration);
    }

    /// <summary>
    /// Initializes size of the trigger that is responsible for rubber banding camera
    /// </summary>
    /// <param name="wallJumpingGameManager"></param>
    private void InitializeRubberBandAreaSize(WallJumpingGameManager wallJumpingGameManager)
    {
        Vector2 restrictedAreaPosition = gameAreaRectTransform.position;
        Vector2 restrictedAreaSize = gameAreaRectTransform.sizeDelta;
        cameraRubberBand.Initialize(wallJumpingGameManager, restrictedAreaPosition, restrictedAreaSize);
    }
    private void InitializeFloor()
    {
        WallJumpingGameManager.Instance.DisableEnviromentMovement();
        Instantiate(floor, movableObjectsContainer.transform);
    }
}
