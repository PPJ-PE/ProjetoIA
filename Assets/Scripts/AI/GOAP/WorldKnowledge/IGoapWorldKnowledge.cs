using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public interface IGoapWorldKnowledge
    {
        public bool GetWorldKnowledge(WorldInfo worldInfo, out object value);
    }
}
