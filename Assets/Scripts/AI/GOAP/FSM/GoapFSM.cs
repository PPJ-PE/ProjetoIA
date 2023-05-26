using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public class GoapFSM
    {
        private IGoapFSMState currentState;
        public void ChangeState(IGoapFSMState newState)
        {
            currentState.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter();
        }
    }
}
