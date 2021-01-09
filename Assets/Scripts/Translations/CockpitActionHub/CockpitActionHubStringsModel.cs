public class CockpitActionHubStringsModel : BaseStringsModel
{
    private const string SETTINGS_KEY = "Settings";

    public string Settings { get => GetString(SETTINGS_KEY); }

    protected override BaseStringsKeeper stringsKeeperInstance => CockpitActionHubTranslationsKeeper.Instance;

    public CockpitActionHubStringsModel() : base() { }
}
