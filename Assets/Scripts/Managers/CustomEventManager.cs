public static class CustomEventManager
{
    #region Delegates
    public delegate void OnDefault();
    #endregion

    #region Events
    public static event OnDefault EventOnDefault;
    #endregion

    #region Public methods
    public static void CallOnDefault()
    {
        if (EventOnDefault != null)
            EventOnDefault.Invoke();
    }
    #endregion
}