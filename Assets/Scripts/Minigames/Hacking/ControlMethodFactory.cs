public static class ControlMethodFactory
{
    /// <summary>
    /// This Method gets control method from PlayerPrefs and return it 
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static BaseControlMethod GetControlMethod(Matrix matrix)
    {
        ControlMethod controlMethod = PlayerPrefsWrapper.GetHackingControlMethod();
        BaseControlMethod result;
        switch (controlMethod)
        {
            case ControlMethod.Click:
                result = new ClickControlMethod(matrix);
                break;
            default:
                result = new SwipeControlMethod(matrix);
                break;
        }
        return result;
    }
}
