using System.Collections.Generic;

public static class ControlMethodExtentions
{
    /// <summary>
    /// Casts ControlMethod to string.
    /// </summary>
    /// <param name="controlMethod"></param>
    /// <returns></returns>
    public static string ToString(this ControlMethod controlMethod)
    {
        Dictionary<ControlMethod, string> controlMethodToStringDictionary = new Dictionary<ControlMethod, string>
        {
            {ControlMethod.Click, "Click"},
            {ControlMethod.Swipe, "Swipe"}
        };
        return controlMethodToStringDictionary[controlMethod];
    }
}