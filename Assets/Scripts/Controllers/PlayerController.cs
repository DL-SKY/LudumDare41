using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    private SpaceObjects thisObject;
    #endregion

    #region Unity method  
    private void Start()
    {
        thisObject = GetComponent<SpaceObjects>();
    }

    private void Update()
    {
        //Вперед
        if (Input.GetKeyDown(KeyCode.W))
        {
            thisObject.ToForward();
        }
        //Тангаж вверх
        else if (Input.GetKeyDown(KeyCode.S))
        {
            thisObject.ToPitchUp();
        }
        //Тангаж вниз
        else if (Input.GetKeyDown(KeyCode.X))
        {
            thisObject.ToPitchDown();
        }
        //Рыскание налево
        else if (Input.GetKeyDown(KeyCode.A))
        {
            thisObject.ToYawLeft();
        }
        //Рыскание направо
        else if (Input.GetKeyDown(KeyCode.D))
        {
            thisObject.ToYawRight();
        }
        //Крен влево
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            thisObject.ToRollLeft();
        }
        //Крен вправо
        else if (Input.GetKeyDown(KeyCode.E))
        {
            thisObject.ToRollRight();
        }

        //1
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            thisObject.ToUse1();
        }
        //2
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            thisObject.ToUse1();
        }
        //3
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            thisObject.ToUse1();
        }
        //4
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            thisObject.ToUse1();
        }
        //5
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            thisObject.ToUse1();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger Enter");
    }
    #endregion

    #region Public methods    
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
