public class NinjaHeroStringsModel : BaseStringsModel
{
    private const string BEST_SCORE_KEY = "BestScore";
    private const string CURRENT_SCORE_KEY = "CurrentScore";
    public string BestScore => GetString(BEST_SCORE_KEY);
    public string CurrentScore => GetString(CURRENT_SCORE_KEY);
    protected override BaseStringsKeeper stringsKeeperInstance => NinjaHeroTranslationsKeeper.Instance;

    public NinjaHeroStringsModel() : base() { }
}
