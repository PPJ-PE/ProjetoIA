using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public abstract class GoapAction : MonoBehaviour
    {
        protected delegate bool Precondition(IGoapWorldKnowledge worldKnowledge);

        protected WorldKnowledge expectedEffects;
        protected Precondition precondition;
        protected int actionCost;

        protected virtual void Awake()
        {
            BuildExpectedEffects();
            BuildPreconditions();
        }

        protected abstract void BuildExpectedEffects();
        protected abstract void BuildPreconditions();
        /// <summary>
        /// Executa a acao
        /// </summary>
        /// <returns>Retorna true quando a acao finalizar</returns>
        public abstract bool RunAction();
        public abstract Vector3 GetActionLocation();
        public int GetActionCost() { return actionCost; }

        public virtual bool IsValid(IGoapWorldKnowledge worldKnowledge)
        {
            if (precondition != null)
            {
                return precondition(worldKnowledge);
            }

            return true;
        }
        public virtual IReadOnlyWorldKnowledge GetExpectedEffects()
        {
            return expectedEffects;
        }
    }
}
