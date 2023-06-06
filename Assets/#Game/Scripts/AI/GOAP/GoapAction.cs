using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public abstract class GoapAction
    {
        protected delegate bool Precondition(IGoapWorldKnowledge worldKnowledge);

        protected WorldKnowledge expectedEffects;
        protected Precondition[] preconditions;
        protected int actionCost;

        public abstract Vector3 GetActionLocation();
        public int GetActionCost() { return actionCost; }

        public virtual bool IsValid(IGoapWorldKnowledge worldKnowledge)
        {
            foreach (Precondition precondition in preconditions)
            {
                if(!precondition(worldKnowledge)) return false;
            }

            return true;
        }
        public virtual IReadOnlyWorldKnowledge GetExpectedEffects()
        {
            return expectedEffects;
        }
    }
}
