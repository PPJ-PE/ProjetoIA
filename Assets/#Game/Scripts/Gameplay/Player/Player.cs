using ProjetoIA.GOAP;
using ProjetoIA.GOAP.PlayerGoals;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace ProjetoIA
{
    public class Player : GoapAgent
    {
        [System.Serializable]
        public class PFields
        {
            public NavMeshAgent NavAgent;
            public Animator animator;
            [ReadOnly]
            public Vector3 CurrentMoveTarget;
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
                ((PlayerAction)action).Player = this;
            }

            sharedKnowledge = new GoapAISharedKnowledge();
            godKnowledge = new GoapAIGodKnowledge();
            aIWorldKnowledge = new GoapAIWorldKnowledge(sharedKnowledge, godKnowledge);
            aIWorldKnowledge.UpdatePersonalKnowledge(bWorldInfo.PlayerAlive, true);


            BuildGoals();
        }

        protected override void Update()
        {
            base.Update();
        }

        public void GoToPosition(Vector3 moveTarget)
        {
            aIWorldKnowledge.UpdatePersonalKnowledge(bWorldInfo.PlayerHasAssignedWaypoint, true);
            pFields.CurrentMoveTarget = moveTarget;
        }

        protected override void IdleState()
        {
            Queue<GoapAction> plan;
            tasks.OrderByDescending((GoapGoal x) => x.GetPriority());
            
            Debug.Log(name + "Esta Planejando...");
            foreach (GoapGoal goal in tasks)
            {
                if (!goal.IsValid(aIWorldKnowledge)) continue;

                plan = goapPlanner.Plan(aIWorldKnowledge, goal);
                if (plan.Count != 0)
                {
                    currentPlan = plan;
                    currentState = GoapFSMStates.MovingTo;
                    pFields.animator.SetTrigger("Run");
                    Debug.Log(name + "Plano encontrado:" + plan.ToString());
                    return;
                }
            }
            
            Debug.LogWarning(name + "Nao encontrou um plano...");
        }

        protected override void MovingToState()
        {
            if (currentPlan.Peek().RunAction())
            {
                aIWorldKnowledge.UpdatePersonalKnowledge(currentPlan.Dequeue().GetExpectedEffects());
                currentState = GoapFSMStates.Idle;
                pFields.animator.SetTrigger("Idle");
            }
        }

        protected override void PerformingAction()
        {
            throw new System.NotImplementedException();
        }
    }
}
