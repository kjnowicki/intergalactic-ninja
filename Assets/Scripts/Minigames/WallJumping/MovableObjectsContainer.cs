using UnityEngine;

public class MovableObjectsContainer : MonoBehaviour
{
    private const string WALL_JUMPING_PLAYER_PREFAB_PATH = "Minigames/WallJumping/Prefabs/Player";
    private WallJumpingPlayer wallJumpingPlayerPrefab;
    private GameObject floor;

    public void Initialize(GameObject floor)
    {
        this.floor = floor;
    }
    
    /// <summary>
    /// Initializes Player.
    /// </summary>
    /// <param name="playerForceScale"></param>
    public void InitializePlayer(float playerForceScale)
    {
        wallJumpingPlayerPrefab = Resources.Load<WallJumpingPlayer>(WALL_JUMPING_PLAYER_PREFAB_PATH);
        float playerInitialYPosition = floor.transform.position.y + WallJumpingGameManager.Instance.playerInitialYPosition;
        Vector2 playerInitialPosition = new Vector2(0, playerInitialYPosition);
        WallJumpingPlayer player = Instantiate(wallJumpingPlayerPrefab, playerInitialPosition, Quaternion.identity, transform);
        player.Initialize(playerForceScale);
    }
}
