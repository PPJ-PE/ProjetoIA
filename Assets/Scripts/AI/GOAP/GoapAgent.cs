using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjetoIA.GOAP
{
    public abstract class GoapAgent : MonoBehaviour
    {
        [SerializeField] protected NavMeshAgent navAgent;

        protected GoapFSM fsm;
        protected IGoapFSMState idle;
        protected IGoapFSMState movingTo;
        protected IGoapFSMState performingAction;

        protected Queue<GoapAction> currentPlan;
        protected List<GoapAction> availableActions;

        protected List<GoapGoal> tasks;
        protected GoapAIWorldKnowledge aIWorldKnowledge;

        private void Awake()
        {
            idle = new IdleState();
            movingTo = new MovingToState();
            performingAction = new PerformingActionState();
        }
        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
    }
}
