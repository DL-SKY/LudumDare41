using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public SpaceObjects thisObject;

    private SpaceController spaceController;    
    #endregion

    #region Unity method  
    private void Start()
    {
        spaceController = SpaceController.Instance;
        thisObject = GetComponent<SpaceObjects>();
    }

    private void Update()
    {
        if (!spaceController.isPlayerTurn)
            return;
        
        //Пропуск
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceController.EndPlayerTurn();
        }
        //Вверх
        else if (Input.GetKeyDown(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            thisObject.ToTop();
            spaceController.EndPlayerTurn();
        }
        //Вниз
        else if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            thisObject.ToBottom();
            spaceController.EndPlayerTurn();
        }
        //Влево
        else if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            thisObject.ToLeft();
            spaceController.EndPlayerTurn();
        }
        //Вправо
        else if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            thisObject.ToRight();
            spaceController.EndPlayerTurn();
        }
        //Вперед
        else if (Input.GetKeyDown(KeyCode.W))
        {
            thisObject.ToForward();
            spaceController.EndPlayerTurn();
        }
        //Тангаж вверх
        else if (Input.GetKeyDown(KeyCode.X))
        {
            thisObject.ToPitchUp();
            spaceController.EndPlayerTurn();
        }
        //Тангаж вниз
        else if (Input.GetKeyDown(KeyCode.S))
        {
            thisObject.ToPitchDown();
            spaceController.EndPlayerTurn();
        }
        //Рыскание налево
        else if (Input.GetKeyDown(KeyCode.A))
        {
            thisObject.ToYawLeft();
            spaceController.EndPlayerTurn();
        }
        //Рыскание направо
        else if (Input.GetKeyDown(KeyCode.D))
        {
            thisObject.ToYawRight();
            spaceController.EndPlayerTurn();
        }
        //Крен влево
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            thisObject.ToRollLeft();
            spaceController.EndPlayerTurn();
        }
        //Крен вправо
        else if (Input.GetKeyDown(KeyCode.E))
        {
            thisObject.ToRollRight();
            spaceController.EndPlayerTurn();
        }

        //1
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            thisObject.ToUse1();
            spaceController.EndPlayerTurn();
        }
        //2
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            thisObject.ToUse2();
            spaceController.EndPlayerTurn();
        }
        //3
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            thisObject.ToUse3();
            spaceController.EndPlayerTurn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AmmoArmy")
        {
            thisObject.countAmmoArmy += other.GetComponent<SpaceObjects>().countAmmoArmy;
            Destroy(other.gameObject);
        }
        else if (other.tag == "AmmoRocket")
        {
            thisObject.countAmmoRocket += other.GetComponent<SpaceObjects>().countAmmoRocket;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Exit")
        {
            spaceController.NewSector();
        }
    }
    #endregion

    #region Public methods    
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
