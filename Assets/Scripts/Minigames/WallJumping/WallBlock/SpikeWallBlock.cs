using System.Collections.Generic;
using UnityEngine;

public class SpikeWallBlock : BaseWallBlock
{
    /// <summary>
    /// When player touches a Spike Wall Block they die.
    /// </summary>
    /// <param name="wallJumpingPlayer"></param>
    public override void OnPlayerTouch(WallJumpingPlayer wallJumpingPlayer)
    {
        wallJumpingPlayer.OnCollected();
    }

    protected override void FlipSprite()
    {
        List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        spriteRenderers.ForEach(it => it.flipX = true);
    }
}
