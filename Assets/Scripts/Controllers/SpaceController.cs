using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : Singleton<SpaceController>
{
    #region Variables
    public bool isInit = false;
    public bool isPlayerTurn = false;    

    //public int maxCountCube;
    public float percentCube = 5.0f;

    public int countVoxels;
    public int countVoid;
    public int countCube;    
    private List<SpaceObjects> objects = new List<SpaceObjects>();
    private Transform space;
    #endregion

    #region Unity methods
    private void Start()
    {
        space = new GameObject("SPACE").transform;

        Generate();
        isInit = true;

        //TODO:
        CustomUIManager.Instance.CloseSplashScreen();
    }
    #endregion

    #region Public methods
    public void EndPlayerTurn()
    {
        isPlayerTurn = false;

        //...

        isPlayerTurn = true;
    }
    #endregion

    #region Private methods
    [ContextMenu("Generate")]
    private void Generate()
    {
        //Очищаем игровое поле
        for (int i = 0; i < space.childCount; i++)
            Destroy(space.GetChild(i).gameObject);

        //isPlayerTurn = false;
        isPlayerTurn = true;
        objects.Clear();

        //Обнуляем счетчики
        countVoxels = 0;
        countVoid = 0;
        countCube = 0;

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

                    spaceMatrix[x, y, z] = (int)random;
                }

        //Применение
        for (int x = 0; x < ConstGame.GAME_BOARD_SIZE; x++)
            for (int y = 0; y < ConstGame.GAME_BOARD_SIZE; y++)
                for (int z = 0; z < ConstGame.GAME_BOARD_SIZE; z++)
                {
                    int x2 = x * ConstGame.CELL_SIZE;
                    int y2 = y * ConstGame.CELL_SIZE;
                    int z2 = z * ConstGame.CELL_SIZE;

                    //Debug.Log(spaceMatrix[x, y, z]);
                    switch (spaceMatrix[x, y, z])
                    {                        
                        case (int)EnumSpaceObject.Void:
                            countVoid++;
                            break;
                        case (int)EnumSpaceObject.Cube:
                            countCube++;
                            Instantiate(Resources.Load<GameObject>(ConstPrefabs.CUBE), new Vector3(x2, y2, z2), Quaternion.identity, space);
                            break;
                    }
                }
    }
    #endregion

    #region Coroutines
    #endregion
}
