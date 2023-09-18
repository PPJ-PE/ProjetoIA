using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public enum PlayerHealthStatus
    {
        Healthy,
        Hurt,
        Dying
    }
    public enum EnemyAlertStatus
    { 
        Patrolling,
        Alert,
        Pursuit,
        Attacking,
        Retreating
    }
    public enum eWorldInfo
    {
        //Player
        PlayerHealthStatus,         //enum (PlayerHealthStatus)
        //Enemies:
        EnemyAlertStatus           //enum (EnemyAlertStatus)
    }
    public enum bWorldInfo
    {
        //Player
        PlayerAlive,                //bool
        PlayerHasAssignedWaypoint,   //bool
        PlayerVisible,              //bool
        Camouflaged,                //bool
        //Equipment:
        RangedWeaponAvailable,      //bool
        GrenageAvailable,           //bool
        DistractionAvailable,       //bool
        //Common:
        Crouching,                  //bool
        IsFiring,                   //bool
        //Enemy:
        EnemyTargetKill
    }
    public enum fWorldInfo
    {
        //Player:
        PlayerDistance = 0,         //float
        EnemyTargetDistance
    }
}
