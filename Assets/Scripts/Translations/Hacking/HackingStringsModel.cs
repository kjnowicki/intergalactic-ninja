public class HackingStringsModel : BaseStringsModel
{
    private const string _EXAMPLE_KEY = "Example";

    public string Example => GetString(_EXAMPLE_KEY);

    protected override BaseStringsKeeper _stringsKeeperInstance => HackingTranslationsKeeper.Instance;

    public HackingStringsModel() : base() { }
}
