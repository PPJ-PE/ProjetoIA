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
        //protected GoapFSMState idle;
        //protected GoapFSMState movingTo;
        //protected GoapFSMState performingAction;

        protected Queue<GoapAction> currentPlan;
        protected List<GoapAction> availableActions;

        protected List<GoapGoal> tasks;
        protected GoapAIWorldKnowledge aIWorldKnowledge;

        private void Start()
        {
            
        }

        private void Awake()
        {
            
        }
        private void Update()
        {
            
        }
    }
}
