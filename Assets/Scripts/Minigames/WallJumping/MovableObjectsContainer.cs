using UnityEngine;

public class MovableObjectsContainer : MonoBehaviour
{
    private const string WALL_JUMPING_PLAYER_PREFAB_PATH = "Minigames/WallJumping/Prefabs/Player";
    
    RectTransform movableObjectsContainerRectTransform;
    private WallJumpingPlayer wallJumpingPlayerPrefab;

    /// <summary>
    /// Initializes MovableObjectsContainer.
    /// </summary>
    /// <param name="gameAreaRectTransform"></param>
    public void Initialize(RectTransform gameAreaRectTransform)
    {
        wallJumpingPlayerPrefab = Resources.Load<WallJumpingPlayer>(WALL_JUMPING_PLAYER_PREFAB_PATH);
        transform.position = gameAreaRectTransform.position;
        movableObjectsContainerRectTransform = GetComponent<RectTransform>();
        movableObjectsContainerRectTransform.sizeDelta = gameAreaRectTransform.sizeDelta;
        movableObjectsContainerRectTransform.position = gameAreaRectTransform.position;
    }

    public void InitializePlayer(float playerForceScale, WallJumpingGameManager wallJumpingGameManager)
    {
        Vector2 playerInitialPosition = Vector2.zero;
        WallJumpingPlayer player = Instantiate(wallJumpingPlayerPrefab, playerInitialPosition, Quaternion.identity, transform);
        player.Initialize(playerForceScale, wallJumpingGameManager);
    }
}
