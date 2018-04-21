using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomUIManager : Singleton<CustomUIManager>
{
    #region Variables
    public Transform parentGUI;
    public Transform parentSplashScreen;

    [SerializeField]
    private List<ScreenController> screens = new List<ScreenController>();
    [SerializeField]
    private List<ScreenController> dialogs = new List<ScreenController>();
    [SerializeField]
    private GameObject splashScreen;
    #endregion

    #region Unity methods
    private void Awake()
    {
        if (!parentGUI)
            parentGUI = transform;
        if (!parentSplashScreen)
            parentSplashScreen = transform;
    }

    private void Start()
    {
        screens.Clear();
        dialogs.Clear();
    }
    #endregion

    #region Public methods
    public void ShowScreen(string _name, object _data = null)
    {
        var path = "Prefabs/Screens";        
        var screen = Instantiate(Resources.Load<GameObject>(Path.Combine(path, _name)), parentGUI).GetComponent<ScreenController>();
        screen.transform.SetAsLastSibling();
        screen.Initialize(_data);

        screens.Add(screen);
    }

    public ScreenController ShowDialog(string _name)
    {
        var screenParent = parentGUI;

        if (screens.Count > 0)
            screenParent = screens[screens.Count - 1].GetComponent<Transform>();

        var path = "Prefabs/Dialogs";
        var dialog = Instantiate(Resources.Load<GameObject>(Path.Combine(path, _name)), screenParent).GetComponent<ScreenController>();
        dialog.transform.SetAsLastSibling();

        dialogs.Add(dialog);

        return dialog;
    }

    public void ShowSplashScreen(string _name)
    {
        StartCoroutine(ShowSplashScreenCoroutine(_name));
    }

    public void CloseDialog(ScreenController _dialog)
    {
        dialogs.Remove(_dialog);
    }

    public void CloseSplashScreen()
    {
        if (splashScreen)
        {
            splashScreen.GetComponent<ScreenController>().CloseSplashScreen();
            splashScreen = null;
        }
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    public IEnumerator ShowSplashScreenCoroutine(string _name)
    {
        if (splashScreen)
            Destroy(splashScreen);

        splashScreen = Instantiate(Resources.Load<GameObject>(_name), parentSplashScreen);
        splashScreen.transform.SetAsLastSibling();

        yield return splashScreen.GetComponent<ScreenController>().WaitShowSplashScreen();
    }
    #endregion
}