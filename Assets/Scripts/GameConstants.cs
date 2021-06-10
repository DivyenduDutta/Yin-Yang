using UnityEngine;

public static class GameConstants
{
    public const float BORDER_LEFT = -5.6f;
    public const float BORDER_RIGHT = 5.6f;
    public const float BORDER_UP = 10f;
    public const float BORDER_DOWN = -10f;

    public enum DIRECTION { 
        LEFT, RIGHT, UP, DOWN, 
        DOWNLEFT, DOWNLEFT1, 
        DOWNLEFT2, DOWNRIGHT, 
        DOWNRIGHT1, DOWNRIGHT2, 
        TOPLEFT, TOPRIGHT, 
        NONE,
        FOLLOW
    }

    public enum EnemyTypes
    {
        Basic,
        EnemyType2,
        BossP1
    }

    public enum Polarity { light, dark };

    public enum GameEvents
    {
        PlayerEntryCutscene,
        PlayerControlling,
        PreGameEvent1,
        GameEvent1,
        TheParallelEvent,
        TheCurtainEnemyEvent,
        ChangeBGEvent,
        BossEvent,
        None
    }

    //level names match the scene names. Make sure to add a new entry made here to LevelLoader/scenes enum
    public enum LevelNames
    {
        TheResistance
    }

    public const string StartPreGameEvent1Tutorial = "StartPreGameEvent1Tutorial";
    public const string StartGameEvent1 = "StartGameEvent1";
    public const string StartParallelEnemyEvent = "StartParallelEnemyEvent";
    public const string StartCurtainEnemyEvent = "StartCurtainEnemyEvent";
    public const string StartChangeBGEvent = "StartChangeBGEvent";
    public const string StartBossEvent = "StartBossEvent";

}
