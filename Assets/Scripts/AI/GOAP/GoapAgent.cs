using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

namespace ProjetoIA.GOAP
{
    public abstract class GoapAgent : MonoBehaviour
    {
        [SerializeField] protected NavMeshAgent navAgent;

        protected GoapFSMStates currentState;

        protected Queue<GoapAction> currentPlan;
        protected GoapAction[] actions;

        protected List<GoapGoal> tasks;
        protected GoapAIWorldKnowledge aIWorldKnowledge;

        private void Start()
        {
            currentState = GoapFSMStates.Idle;
        }

        private void Update()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            switch (currentState)
            {
                case GoapFSMStates.Idle:
                    IdleState();
                    break;
                case GoapFSMStates.MovingTo:
                    MovingToState();
                    break;
                case GoapFSMStates.PerformingAction:
                    PerformingAction();
                    break;
            }
        }

        protected virtual void IdleState()
        {
            tasks.OrderByDescending((GoapGoal x) => x.Priority());
            foreach (GoapGoal goal in tasks)
            {
                
            }
        }
        protected virtual void MovingToState()
        {
            
        }
        protected virtual void PerformingAction()
        {
            
        }
    }
}
