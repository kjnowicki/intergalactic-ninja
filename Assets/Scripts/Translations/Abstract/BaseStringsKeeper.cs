using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

public abstract class BaseStringsKeeper
{
    private const string _STRINGS_RESOURCE_PATH_PATTERN = "Strings/{0}/{1}"; // 0 - path, 1 - language

    protected Language _language;
    protected Dictionary<string, string> _stringsDictionary;
    protected abstract string _TranslationPath { get; }

    private Dictionary<string, string> ReadString(string resourcePath)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(resourcePath);
        XmlDocument xmldoc = new XmlDocument();
        XElement element = XElement.Parse(textAsset.text);
        return element.Elements().ToDictionary(key => key.Name.ToString(), val => val.Value);
    }
    protected BaseStringsKeeper()
    {
        Load();
    }

    private Language LoadLanguage()
    {
        return PlayerPrefsWrapper.GetLanguage();
    }

    private Dictionary<string, string> LoadFromResurce(string translationPath, Language language)
    {
        var fullResourcePath = string.Format(_STRINGS_RESOURCE_PATH_PATTERN, translationPath, language.GetStringValue());
        return ReadString(fullResourcePath);
    }

    private void Load()
    {
        var language = LoadLanguage();
        _language = language;
        _stringsDictionary = LoadFromResurce(_TranslationPath, _language);
    }

    public void Reload()
    {
        var language = LoadLanguage();
        if (_language != language)
        {
            _language = language;
            _stringsDictionary = LoadFromResurce(_TranslationPath, _language);
        }
    }

    public string GetString(string key)
    {
        return _stringsDictionary[key];
    }
}
