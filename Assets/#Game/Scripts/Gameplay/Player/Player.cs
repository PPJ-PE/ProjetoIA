using ProjetoIA.GOAP;
using ProjetoIA.GOAP.PlayerGoals;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjetoIA
{
    public class Player : GoapAgent
    {
        [System.Serializable]
        public class PFields
        {
            [ReadOnly]
            public Vector3 currentMoveTarget;
        }

        [SerializeField] private PFields pFields;

        private GoapAISharedKnowledge sharedKnowledge;
        private GoapAIGodKnowledge godKnowledge;
        private GoapAIWorldKnowledge aIWorldKnowledge;

        private void BuildGoals()
        {
            tasks.Add(new GoToWaypoint());
        }

        protected override void Awake()
        {
            base.Awake();
            foreach (GoapAction action in actions)
            {
                ((PlayerAction)action).PFields = pFields;
            }

            sharedKnowledge = new GoapAISharedKnowledge();
            godKnowledge = new GoapAIGodKnowledge();
            aIWorldKnowledge = new GoapAIWorldKnowledge(sharedKnowledge, godKnowledge);
            
            BuildGoals();
        }

        protected override void Update()
        {
            base.Update();
        }

        public void GoToPosition(Vector3 moveTarget)
        {
            aIWorldKnowledge.UpdatePersonalKnowledge(bWorldInfo.PlayerHasAssignedWaypoint, true);
            pFields.currentMoveTarget = moveTarget;
        }

        protected override void IdleState()
        {
            Queue<GoapAction> plan;
            tasks.OrderByDescending((GoapGoal x) => x.GetPriority());
            
            Debug.Log(name + "Esta Planejando...");
            foreach (GoapGoal goal in tasks)
            {
                plan = goapPlanner.Plan(aIWorldKnowledge, goal);
                if (plan.Count != 0)
                {
                    currentState = GoapFSMStates.MovingTo;
                    Debug.Log(name + "Plano encontrado:" + plan.ToString());
                    return;
                }
            }
            
            Debug.Log(name + "Nao encontrou um plano...");
        }

        protected override void MovingToState()
        {
            throw new System.NotImplementedException();
        }

        protected override void PerformingAction()
        {
            throw new System.NotImplementedException();
        }
    }
}
