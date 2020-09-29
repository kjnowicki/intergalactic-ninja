public class SpikeWallBlock : BaseWallBlock
{
    public override void OnPlayerTouch(WallJumpingPlayer wallJumpingPlayer)
    {
        base.OnPlayerTouch(wallJumpingPlayer);
        wallJumpingPlayer.Die();
    }
}
