using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public abstract class GoapAIKnowledge
    {
        protected Dictionary<WorldInfo, object> knowledge;

        public GoapAIKnowledge()
        {
            knowledge = new Dictionary<WorldInfo, object>();
        }
        public IReadOnlyDictionary<WorldInfo, object> GetWorldKnowledge()
        {
            return knowledge;
        }
        public void UpdateWorldKnowledge(WorldInfo worldInfo, object value)
        {
            if(knowledge.ContainsKey(worldInfo))
                knowledge[worldInfo] = value;
            else 
                knowledge.Add(worldInfo, value);
        }
    }
}
