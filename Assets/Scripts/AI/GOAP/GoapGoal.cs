using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjetoIA.GOAP
{
    public abstract class GoapGoal
    {

        public abstract bool IsValid(IGoapWorldKnowledge worldKnowledge);
        public abstract int Priority();
    }
}
