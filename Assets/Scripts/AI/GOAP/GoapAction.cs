using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public abstract class GoapAction
    {
        protected delegate bool Precondition(IGoapWorldKnowledge worldKnowledge);

        protected Dictionary<WorldInfo, object> expectedEffects;
        protected Precondition[] preconditions;

        public bool IsValid(IGoapWorldKnowledge worldKnowledge)
        {
            foreach (Precondition precondition in preconditions)
            {
                if(!precondition(worldKnowledge)) return false;
            }

            return true;
        }
        public IReadOnlyDictionary<WorldInfo, object> GetExpectedEffects()
        {
            return expectedEffects;
        }
    }
}
