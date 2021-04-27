using System;

internal class NinjaHeroTranslationsKeeper : BaseStringsKeeper
{
    protected override string TranslationPath => "Minigames/NinjaHero"; // path begining from Recources/Strings/

    private static readonly Lazy<NinjaHeroTranslationsKeeper>
        lazy =
        new Lazy<NinjaHeroTranslationsKeeper>
            (() => new NinjaHeroTranslationsKeeper());

    public static NinjaHeroTranslationsKeeper Instance { get { return lazy.Value; } }

    private NinjaHeroTranslationsKeeper() : base() { }
}
