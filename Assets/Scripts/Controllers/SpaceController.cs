using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : Singleton<SpaceController>
{
    #region Variables
    public int countSectors = 0;
    public int countTurns = 0;

    public bool isInit = false;
    public bool isPlayerTurn = false;    

    public float percentCube = 0.5f;
    public float percentEnemies = 0.1f;

    public int countVoxels;
    public int countVoid;
    public int countCube;
    public int countEnemies;
    
    [SerializeField]
    private List<SpaceObjects> objects = new List<SpaceObjects>();
    private Transform space;
    private GameObject exit;
    #endregion

    #region Unity methods
    private void Start()
    {
        space = new GameObject("SPACE").transform;

        CustomUIManager.Instance.ShowScreen(ConstPrefabs.UI_SCREEN_GAME);

        countSectors = 0;
        countTurns = 0;
        Generate();          
    }
    #endregion

    #region Public methods
    public void EndPlayerTurn()
    {
        StartCoroutine(StartAITurn());
    }

    public void AddSpaceObject(SpaceObjects _obj)
    {
        objects.Add(_obj);
    }

    public void NewSector()
    {
        Generate();
    }

    public void DestroyBlock()
    {
        countCube--;
        if (countCube < 1)
            exit.SetActive(true);
    }
    #endregion

    #region Private methods
    [ContextMenu("Generate")]
    private void Generate()
    {
        isInit = false;

        //Очищаем игровое поле
        for (int i = 0; i < space.childCount; i++)
            Destroy(space.GetChild(i).gameObject);

        //isPlayerTurn = false;
        isPlayerTurn = true;
        objects.Clear();

        //Инк. счетчик секторов и ходов
        countSectors++;
        countTurns++;

        //Обнуляем счетчики
        countVoxels = 0;
        countVoid = 0;
        countCube = 0;
        countEnemies = 0;

        //Матрица игрового поля
        int[,,] spaceMatrix = new int[ConstGame.GAME_BOARD_SIZE, ConstGame.GAME_BOARD_SIZE, ConstGame.GAME_BOARD_SIZE];

        //Генерация
        for (int x = 0; x < ConstGame.GAME_BOARD_SIZE; x++)
            for (int y = 0; y < ConstGame.GAME_BOARD_SIZE; y++)
                for (int z = 0; z < ConstGame.GAME_BOARD_SIZE; z++)
                {
                    countVoxels++;

                    float random = Random.Range(0.0f, 100.0f);

                    if (random <= percentCube)
                        random = (float)EnumSpaceObject.Cube;
                    else if (random <= percentCube + percentEnemies + (countSectors/100.0f))
                        random = (float)EnumSpaceObject.Enemy;
                    else
                        random = (float)EnumSpaceObject.Void;

                    spaceMatrix[x, y, z] = (int)random;
                }
        //Точка Игрока
        int x1, y1, z1;
        x1 = Random.Range(2, ConstGame.GAME_BOARD_SIZE-3);
        y1 = Random.Range(2, ConstGame.GAME_BOARD_SIZE-3);
        z1 = Random.Range(2, ConstGame.GAME_BOARD_SIZE-3);
        spaceMatrix[x1, y1, z1] = (int)EnumSpaceObject.StartPlayer;
        //Точка выхода
        int x2, y2, z2;
        x2 = Random.Range(2, ConstGame.GAME_BOARD_SIZE);
        y2 = Random.Range(2, ConstGame.GAME_BOARD_SIZE);
        z2 = Random.Range(2, ConstGame.GAME_BOARD_SIZE);
        while (x1 == x2 || y1 == y2 || z1 == z2)
        {
            x2 = Random.Range(2, ConstGame.GAME_BOARD_SIZE);
            y2 = Random.Range(2, ConstGame.GAME_BOARD_SIZE);
            z2 = Random.Range(2, ConstGame.GAME_BOARD_SIZE);
        }
        spaceMatrix[x2, y2, z2] = (int)EnumSpaceObject.Finish;

        //Применение
        for (int x = 0; x < ConstGame.GAME_BOARD_SIZE; x++)
            for (int y = 0; y < ConstGame.GAME_BOARD_SIZE; y++)
                for (int z = 0; z < ConstGame.GAME_BOARD_SIZE; z++)
                {
                    x2 = x * ConstGame.CELL_SIZE;
                    y2 = y * ConstGame.CELL_SIZE;
                    z2 = z * ConstGame.CELL_SIZE;

                    switch (spaceMatrix[x, y, z])
                    {                        
                        case (int)EnumSpaceObject.Void:
                            countVoid++;
                            break;
                        case (int)EnumSpaceObject.Cube:
                            countCube++;
                            Instantiate(Resources.Load<GameObject>(ConstPrefabs.CUBE), new Vector3(x2, y2, z2), Quaternion.identity, space);
                            break;
                        case (int)EnumSpaceObject.Enemy:
                            countEnemies++;
                            //TODO
                            //var enemy = Instantiate(Resources.Load<GameObject>(ConstPrefabs.ROCKET), new Vector3(x2, y2, z2), Quaternion.identity, space);
                            //AddSpaceObject(enemy.GetComponent<SpaceObjects>());
                            break;
                        case (int)EnumSpaceObject.StartPlayer:
                            FindObjectOfType<PlayerController>().transform.position = new Vector3(x2, y2, z2);
                            Debug.Log("Start: " + x + "/" + y + "/" + z);
                            break;
                        case (int)EnumSpaceObject.Finish:
                            exit = Instantiate(Resources.Load<GameObject>(ConstPrefabs.EXIT), new Vector3(x2, y2, z2), Quaternion.identity, space);
                            exit.SetActive(false);
                            Debug.Log("Finish: " + x + "/" + y + "/" + z);
                            break;
                    }
                }

        CustomEventManager.CallOnChangeTurn();
        CustomEventManager.CallOnEndPlayerTurn(!isPlayerTurn);

        isInit = true;
    }
    #endregion

    #region Coroutines
    private IEnumerator StartAITurn()
    {
        isPlayerTurn = false;
        CustomEventManager.CallOnEndPlayerTurn(!isPlayerTurn);

        //TODO
        if (objects.Count > 0)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] != null)
                {
                    yield return new WaitForSeconds(0.1f);
                    //TODO...
                    objects[i].ToAITurn();
                }
            }

            for (int i = objects.Count-1; i >= 0; i--)
            {
                if (objects[i] == null)
                    objects.RemoveAt(i);
            }
        }

        yield return null;

        isPlayerTurn = true;
        countTurns++;

        CustomEventManager.CallOnChangeTurn();
        CustomEventManager.CallOnEndPlayerTurn(!isPlayerTurn);
    }
    #endregion
}
