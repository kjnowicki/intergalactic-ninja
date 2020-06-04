public class CockpitActionHubStringsModel : BaseStringsModel
{
    private const string _SETTINGS_KEY = "Settings";

    public string Settings { get => GetString(_SETTINGS_KEY); }

    protected override BaseStringsKeeper _stringsKeeperInstance => CockpitActionHubTranslationsKeeper.Instance;

    public CockpitActionHubStringsModel() : base() { }
}
