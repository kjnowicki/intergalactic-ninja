public class NormalWallBlock : BaseWallBlock
{
    /// <summary>
    /// When player touches a Normal Wall Block they are safe.
    /// </summary>
    /// <param name="wallJumpingPlayer"></param>
    public override void OnPlayerTouch(WallJumpingPlayer wallJumpingPlayer) => base.OnPlayerTouch(wallJumpingPlayer);
}
