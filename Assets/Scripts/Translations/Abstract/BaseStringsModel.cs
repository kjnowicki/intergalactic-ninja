public abstract class BaseStringsModel
{
    protected abstract BaseStringsKeeper stringsKeeperInstance { get; }

    protected string GetString(string key) => stringsKeeperInstance.GetString(key);

    public void Reload()
    {
        stringsKeeperInstance.Reload();
    }
}
