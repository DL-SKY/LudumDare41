using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomGameManager : Singleton<CustomGameManager>
{
    #region Variables
    private string currentScene = null;
    #endregion

    #region Unity methods
    private void Start()
    {
        StartCoroutine(StartGame());
    }
    #endregion

    #region Public methods
    public void LoadScene(string _scene, LoadSceneMode _mode = LoadSceneMode.Additive)
    {
        StartCoroutine(LoadSceneCoroutine(_scene, _mode));
    }
    #endregion

    #region Private methods
    private IEnumerator StartGame()
    {
        //Стартовый прелоадер
        yield return CustomUIManager.Instance.ShowSplashScreenCoroutine(ConstPrefabs.UI_SPLASH_STARTING);
        yield return new WaitForSeconds(1.0f);

        LoadScene(ConstScenes.GAME);
        //while (!SpaceController.Instance.isInit)
        
    }

    private IEnumerator LoadSceneCoroutine(string _scene, LoadSceneMode _mode)
    {
        //Выгружаем предыдущую сцену
        if (SceneManager.sceneCount > 1)
        {
            var oldScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            yield return SceneManager.UnloadSceneAsync(oldScene);
        }

        //Загружаем новую сцену
        currentScene = _scene;
        yield return SceneManager.LoadSceneAsync(currentScene, _mode);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene));
    }
    #endregion
}
