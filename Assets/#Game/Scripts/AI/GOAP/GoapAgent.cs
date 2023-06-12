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
        [SerializeField, ReadOnly] protected GoapAction[] actions;

        [SerializeField, ReadOnly] protected GoapFSMStates currentState;

        protected Queue<GoapAction> currentPlan;
        protected GoapPlanner goapPlanner;

        protected List<GoapGoal> tasks;

        [EditorCools.Button]
        protected void FindGoapActions()
        {
            actions = GetComponents<GoapAction>();
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

        protected virtual void Awake()
        {
            currentState = GoapFSMStates.Idle;
            goapPlanner = new GoapPlanner(actions);
            tasks = new List<GoapGoal>();
        }

        protected virtual void Update()
        {
            UpdateState();
        }

        protected abstract void IdleState();
        protected abstract void MovingToState();
        protected abstract void PerformingAction();
    }
}
