using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjects : MonoBehaviour
{
    #region Variables
    public int healthCurrent;
    public int healthMax = 5;

    public int countAmmoArmy = 0;
    public int countAmmoRocket = 0;

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

        healthCurrent = healthMax;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tag == "Rocket")
        {
            var obj = other.GetComponent<SpaceObjects>();
            if (obj)
                obj.GetDamage(ConstGame.DAMAGE_U3);

            healthCurrent = 1;
            GetDamage(1);
        }
    }

    private void OnDestroy()
    {
        
    }
    #endregion

    #region Public methods
    public void ToForwardRocket()
    {
        if (!isEndAnimation)
            return;

        var newPosition = transform.position + transform.forward * ConstGame.CELL_SIZE;

        newPosition = new Vector3(  Mathf.RoundToInt(newPosition.x),
                                    Mathf.RoundToInt(newPosition.y),
                                    Mathf.RoundToInt(newPosition.z) );
        rg.MovePosition(newPosition);
    }

    public void ToForward()
    {
        if (!isEndAnimation)
            return;

        if (CheckToHitToOneVoxel(transform.TransformDirection(Vector3.forward)))
            return;

        var newPosition = transform.position + transform.forward * ConstGame.CELL_SIZE;

        if (CheckToEndBoard(newPosition))
            return;

        newPosition = new Vector3(  Mathf.RoundToInt(newPosition.x),
                                    Mathf.RoundToInt(newPosition.y),
                                    Mathf.RoundToInt(newPosition.z) );
        rg.MovePosition(newPosition);
    }

    public void ToLeft()
    {
        if (!isEndAnimation)
            return;

        if (CheckToHitToOneVoxel(transform.TransformDirection(Vector3.left)))
            return;

        var newPosition = transform.position - transform.right * ConstGame.CELL_SIZE;

        if (CheckToEndBoard(newPosition))
            return;

        newPosition = new Vector3(  Mathf.RoundToInt(newPosition.x),
                                    Mathf.RoundToInt(newPosition.y),
                                    Mathf.RoundToInt(newPosition.z) );
        rg.MovePosition(newPosition);
    }

    public void ToRight()
    {
        if (!isEndAnimation)
            return;

        if (CheckToHitToOneVoxel(transform.TransformDirection(Vector3.right)))
            return;

        var newPosition = transform.position + transform.right * ConstGame.CELL_SIZE;

        if (CheckToEndBoard(newPosition))
            return;

        newPosition = new Vector3(  Mathf.RoundToInt(newPosition.x),
                                    Mathf.RoundToInt(newPosition.y),
                                    Mathf.RoundToInt(newPosition.z) );
        rg.MovePosition(newPosition);
    }

    public void ToTop()
    {
        if (!isEndAnimation)
            return;

        if (CheckToHitToOneVoxel(transform.TransformDirection(Vector3.up)))
            return;

        var newPosition = transform.position + transform.up * ConstGame.CELL_SIZE;

        if (CheckToEndBoard(newPosition))
            return;

        newPosition = new Vector3(  Mathf.RoundToInt(newPosition.x),
                                    Mathf.RoundToInt(newPosition.y),
                                    Mathf.RoundToInt(newPosition.z) );
        rg.MovePosition(newPosition);
    }

    public void ToBottom()
    {
        if (!isEndAnimation)
            return;

        if (CheckToHitToOneVoxel(transform.TransformDirection(Vector3.down)))
            return;

        var newPosition = transform.position - transform.up * ConstGame.CELL_SIZE;

        if (CheckToEndBoard(newPosition))
            return;

        newPosition = new Vector3(Mathf.RoundToInt(newPosition.x),
                                    Mathf.RoundToInt(newPosition.y),
                                    Mathf.RoundToInt(newPosition.z));
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

    //Буровой лазер
    public void ToUse1()
    {
        if (!isEndAnimation)
            return;

        var ray = Instantiate(Resources.Load<GameObject>(ConstPrefabs.PARTICLES_LINE_MINING));
        Destroy(ray, 0.25f);
        var line = ray.GetComponent<LineRenderer>();
        if (line)
        {
            line.SetPosition(0, transform.position);
            var newPosition = transform.position + transform.forward * ConstGame.CELL_SIZE * ConstGame.RAY_COEF_U1;
            line.SetPosition(1, newPosition);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, ConstGame.RAY_COEF_U1 * ConstGame.CELL_SIZE))
        {
            //if (hit.collider.tag == "Block")
            //{
                var controller = hit.collider.GetComponent<SpaceObjects>();
                if (controller)
                    controller.GetDamage(ConstGame.DAMAGE_U1);
            //}
        }
    }

    //Армейский лазер
    public void ToUse2()
    {
        if (!isEndAnimation)
            return;

        if (countAmmoArmy <= 0)
        {
            countAmmoArmy = 0;
            return;
        }
        countAmmoArmy--;

        var ray = Instantiate(Resources.Load<GameObject>(ConstPrefabs.PARTICLES_LINE_ARMY));
        Destroy(ray, 0.25f);
        var line = ray.GetComponent<LineRenderer>();
        if (line)
        {
            line.SetPosition(0, transform.position);
            var newPosition = transform.position + transform.forward * ConstGame.CELL_SIZE * ConstGame.RAY_COEF_U2;
            line.SetPosition(1, newPosition);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, ConstGame.RAY_COEF_U2 * ConstGame.CELL_SIZE))
        {
            //if (hit.collider.tag == "Block")
            //{
            var controller = hit.collider.GetComponent<SpaceObjects>();
            if (controller)
                controller.GetDamage(ConstGame.DAMAGE_U2);
            //}
        }
    }

    public void ToUse3()
    {
        if (!isEndAnimation)
            return;

        if (countAmmoRocket <= 0)
        {
            countAmmoRocket = 0;
            return;
        }
        countAmmoRocket--;

        var newPosition = transform.position + transform.forward * ConstGame.CELL_SIZE;
        newPosition = new Vector3(  Mathf.RoundToInt(newPosition.x),
                                    Mathf.RoundToInt(newPosition.y),
                                    Mathf.RoundToInt(newPosition.z) );

        var rocket = Instantiate(Resources.Load<GameObject>(ConstPrefabs.ROCKET), newPosition, transform.rotation, transform.parent);
        var obj = rocket.GetComponent<SpaceObjects>();
        if (obj)
            spaceController.AddSpaceObject(obj);
    }

    public void ToUse4()
    {

    }

    public void ToUse5()
    {

    }

    public void ToAITurn()
    {
        if (tag == "Rocket")
        {
            GetDamage(1);
            ToForwardRocket();
        }
    }

    public void GetDamage(int _damage)
    {
        if (tag == "AmmoArmy" || tag == "AmmoRocket")
            return;

        healthCurrent -= _damage;

        if (tag != "Rocket")
        {
            var boom = Instantiate(Resources.Load<GameObject>(ConstPrefabs.PARTICLES_VOXELS), transform.position, Quaternion.identity, transform.parent);
            Destroy(boom, 5.0f);
        }        

        if (healthCurrent <= 0)
        {
            //TODO:
            if (tag == "Block")
            {
                int dice = (Random.Range(6, 101) % 6) + 1;
                Debug.Log("Dice: " + dice);

                switch (dice)
                {
                    case 1:
                        Instantiate(Resources.Load<GameObject>(ConstPrefabs.AMMO_ARMY), transform.position, Quaternion.identity, transform.parent);
                        break;
                    case 6:
                        Instantiate(Resources.Load<GameObject>(ConstPrefabs.AMMO_ROCKET), transform.position, Quaternion.identity, transform.parent);
                        break;
                }

                spaceController.DestroyBlock();
            }
            else if (tag == "Rocket")
            {
                var boom = Instantiate(Resources.Load<GameObject>(ConstPrefabs.PARTICLES_ROCKET_BOOM), transform.position, Quaternion.identity, transform.parent);
                Destroy(boom, 5.0f);
            }

            if (tag != "Player")
                Destroy(this.gameObject);
        }        
    }
    #endregion

    #region Private methods
    private bool CheckToHitToOneVoxel(Vector3 _direction)
    {
        bool result = false;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, _direction, ConstGame.RAY_COEF * ConstGame.CELL_SIZE);

        foreach (var hit in hits)
        {
            if (hit.rigidbody != rg)
            {
                if (hit.rigidbody.tag != "AmmoArmy" && hit.rigidbody.tag != "AmmoRocket")
                {
                    result = true;
                    break;
                }                
            }
        }

        return result;
    }

    private bool CheckToEndBoard(Vector3 _newPosition)
    {
        if (Mathf.RoundToInt(_newPosition.x) < 0 || Mathf.RoundToInt(_newPosition.x) > ConstGame.CELL_SIZE * ConstGame.GAME_BOARD_SIZE)
            return true;
        if (Mathf.RoundToInt(_newPosition.y) < 0 || Mathf.RoundToInt(_newPosition.y) > ConstGame.CELL_SIZE * ConstGame.GAME_BOARD_SIZE)
            return true;
        if (Mathf.RoundToInt(_newPosition.z) < 0 || Mathf.RoundToInt(_newPosition.z) > ConstGame.CELL_SIZE * ConstGame.GAME_BOARD_SIZE)
            return true;

        return false;
    }
    #endregion

    #region Coroutines
    #endregion
}
