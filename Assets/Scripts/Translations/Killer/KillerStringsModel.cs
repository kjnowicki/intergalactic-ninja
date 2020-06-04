public class KillerStringsModel : BaseStringsModel
{
    private const string _EXAMPLE_KEY = "Example";

    public string Example => GetString(_EXAMPLE_KEY);

    protected override BaseStringsKeeper _stringsKeeperInstance => KillerTranslationsKeeper.Instance;

    public KillerStringsModel() : base() { }
}
