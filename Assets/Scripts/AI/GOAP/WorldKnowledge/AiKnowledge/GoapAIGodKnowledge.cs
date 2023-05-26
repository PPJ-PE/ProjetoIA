using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public class GoapAIGodKnowledge : GoapAIKnowledge
    {
        private static GoapAIGodKnowledge instance;
        public GoapAIGodKnowledge() 
        {
            if (instance != null)
                Debug.LogError("Multiple god knowledge detected!");
            else 
                instance = this;
        }
    }
}
