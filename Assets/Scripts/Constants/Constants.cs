using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumSpaceObject
{
    Void = 0,
    Cube = 1,
    StartPlayer = 2,
    Enemy = 3,
    Finish = 4,
}

public static class ConstGame
{
    public static int CELL_SIZE = 10;               //сторона вокселя игрового пространства
    public static float RAY_COEF = 1.1f;            //длина луча для проверки столкновения
    public static int GAME_BOARD_SIZE = 15;         //ширина игрового поля в вокселях

    public static float RAY_COEF_U1 = 3;            //длина луча оружия 1 (буровой лазер)
    public static float RAY_COEF_U2 = 9;            //длина луча оружия 1 (армейский лазер)

    public static int DAMAGE_U1 = 1;                //урон от оружия 1 (буровой лазер)
    public static int DAMAGE_U2 = 2;                //урон от оружия 1 (армейский лазер)
    public static int DAMAGE_U3 = 10;               //урон от оружия 1 (ракета)
}

public static class ConstScenes
{
    public static string GAME = "Game";
}

public static class ConstPrefabs
{
    //Космос
    public static string CUBE = "Prefabs/Space/Cube";
    public static string AMMO_ARMY = "Prefabs/Space/AmmoArmy";
    public static string AMMO_ROCKET = "Prefabs/Space/AmmoRocket";
    public static string ROCKET = "Prefabs/Space/Rocket";
    public static string EXIT = "Prefabs/Space/Exit";


    //Частицы
    public static string PARTICLES_VOXELS = "Prefabs/Particles/VoxelsBoom";
    public static string PARTICLES_ROCKET_BOOM = "Prefabs/Particles/RocketBoom";
    public static string PARTICLES_LINE_MINING = "Prefabs/Particles/LineMiningLaser";
    public static string PARTICLES_LINE_ARMY = "Prefabs/Particles/LineArmyLaser";

    //UI: Splashscreen
    public static string UI_SPLASH_STARTING = "Prefabs/UI/Splash/StartingSplash";
    public static string UI_SPLASH_HISTORY = "Prefabs/UI/Splash/HistorySplash";

    //UI: Screens
    public static string UI_SCREEN_GAME = "Prefabs/UI/Screen/GameScreen";
}
