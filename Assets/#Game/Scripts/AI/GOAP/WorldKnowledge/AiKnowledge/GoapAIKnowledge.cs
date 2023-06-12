using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public abstract class GoapAIKnowledge
    {
        private WorldKnowledge worldKnowledge;

        public GoapAIKnowledge()
        {
            worldKnowledge = new WorldKnowledge();
        }
        public WorldKnowledge GetWorldKnowledge()
        {
            return worldKnowledge;
        }
        public void UpdateWorldKnowledge(bWorldInfo bWorldInfo, bool value)
        {
            if(worldKnowledge.ContainsKey(bWorldInfo))
                worldKnowledge[bWorldInfo] = value;
            else
                worldKnowledge.Add(bWorldInfo, value);
        }
        public void UpdateWorldKnowledge(fWorldInfo fWorldInfo, float value)
        {
            if (worldKnowledge.ContainsKey(fWorldInfo))
                worldKnowledge[fWorldInfo] = value;
            else
                worldKnowledge.Add(fWorldInfo, value);
        }
        public void UpdateWorldKnowledge(eWorldInfo eWorldInfo, int value)
        {
            if (worldKnowledge.ContainsKey(eWorldInfo))
                worldKnowledge[eWorldInfo] = value;
            else
                worldKnowledge.Add(eWorldInfo, value);
        }
        public void UpdateWorldKnowledge(IReadOnlyWorldKnowledge knowledgeToUpdate)
        {
            foreach (KeyValuePair<bWorldInfo, bool> kvp in (IReadOnlyDictionary<bWorldInfo, bool>)knowledgeToUpdate)
            {
                UpdateWorldKnowledge(kvp.Key, kvp.Value);
            }
            foreach (KeyValuePair<eWorldInfo, int> kvp in (IReadOnlyDictionary<eWorldInfo, int>)knowledgeToUpdate)
            {
                UpdateWorldKnowledge(kvp.Key, kvp.Value);
            }
            foreach (KeyValuePair<fWorldInfo, float> kvp in (IReadOnlyDictionary<fWorldInfo, float>)knowledgeToUpdate)
            {
                UpdateWorldKnowledge(kvp.Key, kvp.Value);
            }
        }
    }
}
