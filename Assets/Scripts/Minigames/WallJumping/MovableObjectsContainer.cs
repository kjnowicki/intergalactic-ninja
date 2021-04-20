using UnityEngine;

public class MovableObjectsContainer : MonoBehaviour
{
    private const string WALL_JUMPING_PLAYER_PREFAB_PATH = "Minigames/WallJumping/Prefabs/Player";
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
        Floor floor = FindObjectOfType<Floor>();
        float playerInitialYPosition = floor.transform.position.y + WallJumpingGameManager.Instance.playerInitialYPosition;
        Vector2 playerInitialPosition = new Vector2(0, playerInitialYPosition);
        WallJumpingPlayer player = Instantiate(wallJumpingPlayerPrefab, playerInitialPosition, Quaternion.identity, transform);
        player.Initialize(playerForceScale, wallJumpingGameManager);
    }
}
