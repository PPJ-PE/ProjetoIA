using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public interface IGoapWorldKnowledge
    {
        public bool GetWorldKnowledge(bWorldInfo bWorldInfo, out bool value);
        public bool GetWorldKnowledge(fWorldInfo fWorldInfo, out float value);
        public bool GetWorldKnowledge(eWorldInfo eWorldInfo, out int value);
    }
}
