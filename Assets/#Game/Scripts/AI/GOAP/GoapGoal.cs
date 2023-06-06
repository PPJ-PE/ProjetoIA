using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjetoIA.GOAP
{
    public abstract class GoapGoal
    {
        private WorldKnowledge _goapGoal;
        public abstract bool IsValid(IGoapWorldKnowledge worldKnowledge);
        public abstract int Priority();
        public virtual WorldKnowledge Objective() { return _goapGoal; }
    }
}
