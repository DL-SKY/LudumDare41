using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Variables
    private static T instance;
    #endregion

    #region Properties
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<T>();

            if (instance == null)
            {
                var singletonObj = new GameObject(typeof(T).FullName);
                instance = singletonObj.AddComponent<T>();
            }                

            return Singleton<T>.instance;
        }
    }
    #endregion

    #region Unity methods
    private void OnDestroy()
    {
        instance = null;
    }
    #endregion
}