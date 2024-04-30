using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    //==================TAG=================
    public const string PlayerTag = "Player";
    public const string EnemyTag = "Enemy";
    public const string ManagerTag = "Manager";
    
    //==============AUDIO NAME==============
    public const string Bgm = "BackgroundMusic";
    public const string LaserGun = "LaserGun";
    public const string LaserHit = "LaserHit";
    public const string Blade = "Blade";
    public const string BladeHit = "BladeHit";
    public const string Slam = "Slam";

    //=============AI STATE NAME=============
    public const string AI_Spawn = "Spawn";
    public const string AI_Idle = "Idle";
    public const string AI_Pursue = "Pursue";
    public const string AI_Attack = "Attack";
    public const string AI_Dead = "Dead";

    public enum  EnemyType
    {
        Range, Melee
    }
}
