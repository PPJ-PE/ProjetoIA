using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public interface IGoapFSMState
    {
        void OnStateEnter();
        void OnStateExit();
        void Update();
    }
}
