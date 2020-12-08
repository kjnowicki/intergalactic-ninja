public class HackingStringsModel : BaseStringsModel
{
    private const string BEST_SCORE_KEY = "BestScore";
    private const string CURRENT_SCORE_KEY = "CurrentScore";
    private const string TIME_KEY = "Time";
    private const string RESET_MESSAGE = "ResetMessage";
    private const string RESET_ACCEPT = "ResetAccept";
    private const string RESET_CANCEL = "ResetCancel";
    public string BestScore => GetString(BEST_SCORE_KEY);
    public string CurrentScore => GetString(CURRENT_SCORE_KEY);
    public string Time => GetString(TIME_KEY);
    public string ResetMessage => GetString(RESET_MESSAGE);
    public string ResetAccept => GetString(RESET_ACCEPT);
    public string ResetCancel => GetString(RESET_CANCEL);
    protected override BaseStringsKeeper stringsKeeperInstance => HackingTranslationsKeeper.Instance;
    public HackingStringsModel() : base() { }
}
