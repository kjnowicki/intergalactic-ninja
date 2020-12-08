using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPopupStringsModel : BaseStringsModel
{
    private const string EXIT_MESSAGE = "ExitMessage";
    public string ExitMessage => GetString(EXIT_MESSAGE);
    protected override BaseStringsKeeper stringsKeeperInstance => ReturnPopupTranslationsKeeper.Instance;

    public ReturnPopupStringsModel() : base() { }
}
