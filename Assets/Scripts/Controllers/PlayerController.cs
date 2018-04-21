using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Rigidbody rg;
    #endregion

    #region Unity method
    private void Start()
    {
        rg = GetComponent<Rigidbody>();

        if (rg == null)
            rg = gameObject.AddComponent<Rigidbody>();
    }

    private void Update()
    {
        //Вперед
        if (Input.GetKeyDown(KeyCode.W))
        {
            ToForward();
        }
        //Назад
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ToBackward();
        }
        //Повернуться налево
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ToLeft();
        }
        //Повернуться направо
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ToRight();
        }
    }
    #endregion

    #region Public methods
    public void ToForward()
    {
        Debug.Log("Forward");

        var newPosition = transform.position + transform.forward * Constants.cellSize;
        newPosition = new Vector3(  Mathf.RoundToInt(newPosition.x),
                                    Mathf.RoundToInt(newPosition.y),
                                    Mathf.RoundToInt(newPosition.z)  );
        rg.MovePosition(newPosition);
    }

    public void ToBackward()
    {
        Debug.Log("Backward");

        var newPosition = transform.position - transform.forward * Constants.cellSize;
        newPosition = new Vector3(  Mathf.RoundToInt(newPosition.x),
                                    Mathf.RoundToInt(newPosition.y),
                                    Mathf.RoundToInt(newPosition.z)  );
        rg.MovePosition(newPosition);
    }

    public void ToLeft()
    {
        Debug.Log("Left");

        var deltaRotation = Quaternion.AngleAxis(-90, Vector3.up);
        rg.rotation *= deltaRotation;
    }

    public void ToRight()
    {
        Debug.Log("Right");

        var deltaRotation = Quaternion.AngleAxis(90, Vector3.up);
        rg.rotation *= deltaRotation;
    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
