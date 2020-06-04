using System;

internal class KillerTranslationsKeeper : BaseStringsKeeper
{
    protected override string _TranslationPath => "Minigames/Killer"; // path begining from Recources/Strings/

    private static readonly Lazy<KillerTranslationsKeeper>
        lazy =
        new Lazy<KillerTranslationsKeeper>
            (() => new KillerTranslationsKeeper());

    public static KillerTranslationsKeeper Instance { get { return lazy.Value; } }

    private KillerTranslationsKeeper() : base() { }
}
