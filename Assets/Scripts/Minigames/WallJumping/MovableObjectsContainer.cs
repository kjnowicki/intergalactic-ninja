using UnityEngine;

public class MovableObjectsContainer : MonoBehaviour
{
    private const string WALL_JUMPING_PLAYER_PREFAB_PATH = "Minigames/WallJumping/Prefabs/Player";
    [SerializeField] private int playerPositionY = -5;
    RectTransform movableObjectsContainerRectTransform;
    private WallJumpingPlayer wallJumpingPlayerPrefab;

    /// <summary>
    /// Initializes Player.
    /// </summary>
    /// <param name="playerForceScale"></param>
    /// <param name="wallJumpingGameManager"></param>
    public void InitializePlayer(float playerForceScale, WallJumpingGameManager wallJumpingGameManager)
    {
        wallJumpingPlayerPrefab = Resources.Load<WallJumpingPlayer>(WALL_JUMPING_PLAYER_PREFAB_PATH);
        Vector2 playerInitialPosition = new Vector2(0, playerPositionY);
        WallJumpingPlayer player = Instantiate(wallJumpingPlayerPrefab, playerInitialPosition, Quaternion.identity, transform);
        player.Initialize(playerForceScale, wallJumpingGameManager);
    }
}
