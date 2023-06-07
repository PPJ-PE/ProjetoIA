using ProjetoIA.GOAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjetoIA
{
    [RequireComponent(typeof(Player))]
    public class PlayerMoveTo : PlayerAction
    {
        private NavMeshAgent playerAgent;

        protected override void Awake()
        {
            base.Awake();
        }
        protected virtual void Start()
        {
            playerAgent = PFields.NavAgent;
        }
        protected override void BuildExpectedEffects()
        {
            expectedEffects = new WorldKnowledge
            {
                { bWorldInfo.PlayerHasAssignedWaypoint, false }
            };
        }

        protected override void BuildPreconditions()
        {
            precondition = (worldKnowledge) =>
            {
                bool value1 = false;
                bool value2 = false;

                worldKnowledge.GetWorldKnowledge(bWorldInfo.PlayerAlive, out value1);
                worldKnowledge.GetWorldKnowledge(bWorldInfo.PlayerHasAssignedWaypoint, out value2);

                return value1 && value2;
            };
        }
        public override Vector3 GetActionLocation()
        {
            return Player.transform.position;
        }

        public override bool RunAction()
        {
            if (playerAgent.hasPath)
            {
                if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
                {
                    if (!playerAgent.hasPath || playerAgent.velocity.sqrMagnitude == 0f)
                    {
                        playerAgent.isStopped = true;
                        playerAgent.ResetPath();
                        return true;
                    }
                }
            }
            else if(!playerAgent.pathPending)
            {
                if (!playerAgent.SetDestination(PFields.CurrentMoveTarget))
                {
                    Debug.LogWarning("No path could be calculated");
                    return true;
                }
            }
            return false;
        }

    }
}
