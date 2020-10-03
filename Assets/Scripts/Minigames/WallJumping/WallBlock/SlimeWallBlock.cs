public class SlimeWallBlock : BaseWallBlock
{
    public override void OnPlayerTouch(WallJumpingPlayer wallJumpingPlayer)
    {
        //base.OnPlayerTouch(wallJumpingPlayer);
        wallJumpingPlayer.Slime();
    }
}
