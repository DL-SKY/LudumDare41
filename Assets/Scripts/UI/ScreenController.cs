using System.Collections;
using UnityEngine;

public enum ScreenType
{
    Screen = 0,
    Dialog = 1,
    SplashScreen = 2,
}

public class ScreenController : MonoBehaviour
{
    #region Variables
    public ScreenType type;

    public bool result = true;          //Only Dialog
    private bool isInit = false;
    private bool isOpened = true;
    #endregion

    #region Properties
    public bool IsInit
    {
        get { return isInit; }
        set { isInit = value; }
    }
    #endregion

    #region Unity methods
    private void Start()
    {
        if (type == ScreenType.SplashScreen)
            Initialize();
    }
    #endregion

    #region Public methods
    public virtual void Initialize(object _data = null)
    {
        isInit = true;
    }

    public virtual void CloseScreen()
    {
        isOpened = false;
    }

    public virtual void CloseDialog(bool _result)
    {
        result = _result;
        isOpened = false;

        CustomUIManager.Instance.CloseDialog(this);

        Destroy(gameObject);
    }

    public void CloseSplashScreen()
    {
        result = true;
        isOpened = false;

        Destroy(gameObject);
    }
    #endregion

    #region Coroutine
    public IEnumerator Wait()
    {
        while (isOpened)
            yield return null;
    }

    public IEnumerator WaitShowSplashScreen()
    {
        while (!isInit)
            yield return null;
    }
    #endregion
}