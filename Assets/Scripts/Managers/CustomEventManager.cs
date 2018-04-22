public static class CustomEventManager
{
    #region Delegates
    public delegate void OnDefault();

    public delegate void OnChangeTurn();
    public delegate void OnEndPlayerTurn(bool _value);
    #endregion

    #region Events
    public static event OnDefault EventOnDefault;

    public static event OnChangeTurn EventOnChangeTurn;
    public static event OnEndPlayerTurn EventOnEndPlayerTurn;
    #endregion

    #region Public methods
    public static void CallOnDefault()
    {
        if (EventOnDefault != null)
            EventOnDefault.Invoke();
    }

    public static void CallOnChangeTurn()
    {
        if (EventOnChangeTurn != null)
            EventOnChangeTurn.Invoke();
    }

    public static void CallOnEndPlayerTurn(bool _value)
    {
        if (EventOnEndPlayerTurn != null)
            EventOnEndPlayerTurn.Invoke(_value);
    }
    #endregion
}