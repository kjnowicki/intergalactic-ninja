public abstract class BaseStringsModel
{
    protected abstract BaseStringsKeeper _stringsKeeperInstance { get; }

    protected string GetString(string key) => _stringsKeeperInstance.GetString(key);

    public void Reload()
    {
        _stringsKeeperInstance.Reload();
    }
}
