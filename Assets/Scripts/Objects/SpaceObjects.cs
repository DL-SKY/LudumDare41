using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjects : MonoBehaviour
{
    #region Variables
    private SpaceController spaceController;
    private Rigidbody rg;
    private bool isEndAnimation = true;
    #endregion

    #region Unity methods
    private void Start()
    {
        spaceController = SpaceController.Instance;

        rg = GetComponent<Rigidbody>();

        if (rg == null)
            rg = gameObject.AddComponent<Rigidbody>();
    }
    #endregion

    #region Public methods
    public void ToForward()
    {
        if (!isEndAnimation)
            return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, ConstGame.RAY_COEF * ConstGame.CELL_SIZE))
        {
            return;
        }

        var newPosition = transform.position + transform.forward * ConstGame.CELL_SIZE;

        if (Mathf.RoundToInt(newPosition.x) < 0 || Mathf.RoundToInt(newPosition.x) > ConstGame.CELL_SIZE * ConstGame.GAME_BOARD_SIZE)
            return;
        if (Mathf.RoundToInt(newPosition.y) < 0 || Mathf.RoundToInt(newPosition.y) > ConstGame.CELL_SIZE * ConstGame.GAME_BOARD_SIZE)
            return;
        if (Mathf.RoundToInt(newPosition.z) < 0 || Mathf.RoundToInt(newPosition.z) > ConstGame.CELL_SIZE * ConstGame.GAME_BOARD_SIZE)
            return;

        newPosition = new Vector3(  Mathf.RoundToInt(newPosition.x),
                                    Mathf.RoundToInt(newPosition.y),
                                    Mathf.RoundToInt(newPosition.z) );
        rg.MovePosition(newPosition);
    }

    public void ToPitchUp()
    {
        if (!isEndAnimation)
            return;


        var deltaRotation = Quaternion.AngleAxis(-90, Vector3.right);
        rg.rotation *= deltaRotation;
    }

    public void ToPitchDown()
    {
        if (!isEndAnimation)
            return;


        var deltaRotation = Quaternion.AngleAxis(90, Vector3.right);
        rg.rotation *= deltaRotation;
    }

    public void ToYawLeft()
    {
        if (!isEndAnimation)
            return;
        
        var deltaRotation = Quaternion.AngleAxis(-90, Vector3.up);
        rg.rotation *= deltaRotation;
    }

    public void ToYawRight()
    {
        if (!isEndAnimation)
            return;

        var deltaRotation = Quaternion.AngleAxis(90, Vector3.up);
        rg.rotation *= deltaRotation;
    }

    public void ToRollLeft()
    {
        if (!isEndAnimation)
            return;

        var deltaRotation = Quaternion.AngleAxis(90, Vector3.forward);
        rg.rotation *= deltaRotation;
    }

    public void ToRollRight()
    {
        if (!isEndAnimation)
            return;

        var deltaRotation = Quaternion.AngleAxis(-90, Vector3.forward);
        rg.rotation *= deltaRotation;
    }

    public void ToUse1()
    {

    }

    public void ToUse2()
    {

    }

    public void ToUse3()
    {

    }

    public void ToUse4()
    {

    }

    public void ToUse5()
    {

    }
    #endregion

    #region Private methods
    #endregion

    #region Coroutines
    #endregion
}
