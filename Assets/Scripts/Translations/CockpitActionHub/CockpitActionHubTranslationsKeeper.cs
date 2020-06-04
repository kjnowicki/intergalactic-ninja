using System;

internal class CockpitActionHubTranslationsKeeper : BaseStringsKeeper
{
    protected override string _TranslationPath => "CockpitActionHub"; // path begining from Recources/Strings/

    private static readonly Lazy<CockpitActionHubTranslationsKeeper>
        lazy =
        new Lazy<CockpitActionHubTranslationsKeeper>
            (() => new CockpitActionHubTranslationsKeeper());

    public static CockpitActionHubTranslationsKeeper Instance { get { return lazy.Value; } }

    private CockpitActionHubTranslationsKeeper() : base() { }
}
