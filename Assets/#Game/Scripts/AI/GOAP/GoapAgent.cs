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
        protected GoapPlanner goapPlanner;

        protected virtual void Awake()
        {
            currentState = GoapFSMStates.Idle;
            goapPlanner = new GoapPlanner(actions);
        }

        protected virtual void Update()
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
            Queue<GoapAction> plan;
            tasks.OrderByDescending((GoapGoal x) => x.Priority());

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
        protected virtual void MovingToState()
        {
            
        }
        protected virtual void PerformingAction()
        {
            
        }
    }
}
