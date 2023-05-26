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
    public enum WorldInfo
    {
        //Player:
        PlayerDistance = 0,         //float
        PlayerAlive,                //bool
        PlayerVisible,              //bool
        PlayerHealthStatus,         //enum (PlayerHealthStatus)
        Camouflaged,                //bool
        //Equipment:
        RangedWeaponAvailable,      //bool
        GrenageAvailable,           //bool
        DistractionAvailable,       //bool
        //Enemies:
        EnemyAlertStatus,           //enum (EnemyAlertStatus)
        //Common:
        Crouching,                  //bool
        IsFiring,                   //bool

    }
}
