using System;

internal class HackingTranslationsKeeper : BaseStringsKeeper
{
    protected override string TranslationPath => "Minigames/Hacking"; // path begining from Recources/Strings/

    private static readonly Lazy<HackingTranslationsKeeper>
        lazy =
        new Lazy<HackingTranslationsKeeper>
            (() => new HackingTranslationsKeeper());

    public static HackingTranslationsKeeper Instance { get { return lazy.Value; } }

    private HackingTranslationsKeeper() : base() { }
}
