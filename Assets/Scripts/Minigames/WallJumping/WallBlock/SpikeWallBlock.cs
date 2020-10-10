public class SpikeWallBlock : BaseWallBlock
{
    /// <summary>
    /// When player touches a Normal Wall Block they die.
    /// </summary>
    /// <param name="wallJumpingPlayer"></param>
    public override void OnPlayerTouch(WallJumpingPlayer wallJumpingPlayer)
    {
        wallJumpingPlayer.Die();
    }
}
