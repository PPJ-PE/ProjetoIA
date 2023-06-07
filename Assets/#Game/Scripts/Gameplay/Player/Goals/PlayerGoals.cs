using ProjetoIA.GOAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP.PlayerGoals
{
    /// <summary>
    /// Tipo um "idle" da IA, vai pro Goal e espera la
    /// </summary>
    public class GoToWaypoint : GoapGoal
    {
        private const int priority = int.MaxValue / 2;
        private WorldKnowledge objective;
        public GoToWaypoint()
        {
            objective = new WorldKnowledge();
            objective.Add(bWorldInfo.PlayerHasAssignedWaypoint, false);
        }
        public override bool IsValid(IGoapWorldKnowledge worldKnowledge)
        {
            bool playerAlive, playerWaypoint;
            worldKnowledge.GetWorldKnowledge(bWorldInfo.PlayerAlive, out playerAlive);
            worldKnowledge.GetWorldKnowledge(bWorldInfo.PlayerHasAssignedWaypoint, out playerWaypoint);

            if (playerAlive && playerWaypoint) return true;
            
            return false;
        }

        public override WorldKnowledge GetObjective()
        {
            return objective;
        }

        public override int GetPriority()
        {
            return priority;
        }
    }
}
