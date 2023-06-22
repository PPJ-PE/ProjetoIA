using ProjetoIA.GOAP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public abstract class GoapSenseBase : MonoBehaviour
    {
        [SerializeField] protected IGoapSenseObserver[] senseObservers;
    }
}
