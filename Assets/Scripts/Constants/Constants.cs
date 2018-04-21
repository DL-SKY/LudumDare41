using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumSpaceObject
{
    Void = 0,
    Cube = 1,
}

public static class ConstGame
{
    public static int CELL_SIZE = 10;               //сторона вокселя игрового пространства
    public static float RAY_COEF = 1.1f;            //длина луча для проверки столкновения
    public static int GAME_BOARD_SIZE = 15;         //ширина игрового поля в вокселях
}

public static class ConstScenes
{
    public static string GAME = "Game";
}

public static class ConstPrefabs
{
    //Космос
    public static string CUBE = "Prefabs/Space/Cube"; 

}
