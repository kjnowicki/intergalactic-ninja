public class SpikeWallBlock : BaseWallBlock
{
    public override void OnPlayerTouch(WallJumpingPlayer wallJumpingPlayer)
    {
        wallJumpingPlayer.Die();
    }
}
