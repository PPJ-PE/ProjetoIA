using ProjetoIA.GOAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public interface IGoapSenseObserver
    {
        void OnWorldStateChange(IReadOnlyWorldKnowledge changes);
    }
}
