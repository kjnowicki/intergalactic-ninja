using System;
internal class ReturnPopupTranslationsKeeper : BaseStringsKeeper
{
    protected override string TranslationPath => "ReturnPopup"; // path begining from Recources/Strings/

    private static readonly Lazy<ReturnPopupTranslationsKeeper>
        lazy =
        new Lazy<ReturnPopupTranslationsKeeper>
            (() => new ReturnPopupTranslationsKeeper());

    public static ReturnPopupTranslationsKeeper Instance { get { return lazy.Value; } }

    private ReturnPopupTranslationsKeeper() : base() { }
}
