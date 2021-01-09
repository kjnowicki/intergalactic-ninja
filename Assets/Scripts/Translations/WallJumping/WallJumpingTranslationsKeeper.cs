using System;

internal class WallJumpingTranslationsKeeper : BaseStringsKeeper
{
    protected override string TranslationPath => "Minigames/WallJumping"; // path begining from Recources/Strings/

    private static readonly Lazy<WallJumpingTranslationsKeeper>
        lazy =
        new Lazy<WallJumpingTranslationsKeeper>
            (() => new WallJumpingTranslationsKeeper());

    public static WallJumpingTranslationsKeeper Instance => lazy.Value;

    private WallJumpingTranslationsKeeper() : base() { }
}
