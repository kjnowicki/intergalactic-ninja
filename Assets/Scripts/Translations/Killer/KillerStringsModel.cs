public class KillerStringsModel : BaseStringsModel
{
    private const string _BEST_SCORE_KEY = "BestScore";
    private const string _CURRENT_SCORE_KEY = "CurrentScore";
    public string BestScore => GetString(_BEST_SCORE_KEY);
    public string CurrentScore => GetString(_CURRENT_SCORE_KEY);
    protected override BaseStringsKeeper _stringsKeeperInstance => KillerTranslationsKeeper.Instance;

    public KillerStringsModel() : base() { }
}
