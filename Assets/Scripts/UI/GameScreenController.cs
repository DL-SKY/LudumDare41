using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenController : ScreenController
{
    #region Variables
    public Text turns;
    public Text sectors;
    public GameObject waiting;

    public GameObject hint;

    public Text ammo;

    private SpaceController spaceController;
    private PlayerController player;
    #endregion

    #region Unity methods
    private void OnEnable()
    {
        CustomEventManager.EventOnChangeTurn += UpdateTurn;
        CustomEventManager.EventOnEndPlayerTurn += ShowWaiting;
    }

    private void OnDisable()
    {
        CustomEventManager.EventOnChangeTurn -= UpdateTurn;
        CustomEventManager.EventOnEndPlayerTurn -= ShowWaiting;
    }
    #endregion

    #region Public methods
    public override void Initialize(object _data = null)
    {
        spaceController = SpaceController.Instance;
        player = FindObjectOfType<PlayerController>();

        base.Initialize(_data);
        StartCoroutine(Show());
    }

    public void CloseHint()
    {
        hint.SetActive(false);
    }
    #endregion

    #region Private methods
    private void UpdateTurn()
    {
        if (!spaceController)
            spaceController = SpaceController.Instance;
        if (!player)
            player = FindObjectOfType<PlayerController>();

        turns.text = "TURN: " + spaceController.countTurns.ToString();
        sectors.text = "SECTOR: " + spaceController.countSectors.ToString();

        ammo.text = string.Format("[ key 1 ] MINING LASER: 999\n[ key 2 ] ARMY LASER: {0}\n[ key 3 ] ROCKETS: {1}", player.thisObject.countAmmoArmy, player.thisObject.countAmmoRocket);
    }

    private void ShowWaiting(bool _value)
    {
        waiting.SetActive(_value);
    }
    #endregion

    #region Coroutines
    private IEnumerator Show()
    {
        while (!spaceController.isInit)
            yield return null;

        if (spaceController.countSectors <= 1)
        {
            //Показываем текст вступления
            yield return CustomUIManager.Instance.ShowSplashScreenCoroutine(ConstPrefabs.UI_SPLASH_HISTORY);

            //Показываем подсказку
            hint.SetActive(true);
        }
        else
        {
            CloseHint();
            CustomUIManager.Instance.CloseSplashScreen();            
        }      
    }
    #endregion
}
