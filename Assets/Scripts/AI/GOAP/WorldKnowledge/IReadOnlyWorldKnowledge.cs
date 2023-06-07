using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public interface IReadOnlyWorldKnowledge : IReadOnlyDictionary<eWorldInfo, int>, IReadOnlyDictionary<fWorldInfo, float>, IReadOnlyDictionary<bWorldInfo, bool>, IGoapWorldKnowledge {
    }
}
