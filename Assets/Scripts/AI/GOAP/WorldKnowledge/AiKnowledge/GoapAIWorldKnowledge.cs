using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA.GOAP
{
    public class GoapAIWorldKnowledge : IGoapWorldKnowledge
    {
        protected GoapAIPersonalKnowledge personalKnowledge;
        protected GoapAISharedKnowledge sharedKnowledge;
        protected GoapAIGodKnowledge godKnowledge;

        protected IReadOnlyDictionary<WorldInfo, object> PersonalDict { get { return personalKnowledge.GetWorldKnowledge(); } }
        protected IReadOnlyDictionary<WorldInfo, object> SharedDict { get { return sharedKnowledge.GetWorldKnowledge(); } }
        protected IReadOnlyDictionary<WorldInfo, object> GodDict { get { return godKnowledge.GetWorldKnowledge(); } }

        public GoapAIWorldKnowledge(GoapAISharedKnowledge sharedKnowledge, GoapAIGodKnowledge godKnowledge)
        {
            personalKnowledge = new GoapAIPersonalKnowledge();
            this.sharedKnowledge = sharedKnowledge;
            this.godKnowledge = godKnowledge;
        }
        public bool GetWorldKnowledge(WorldInfo worldInfo, out object value)
        {
            if (PersonalDict.ContainsKey(worldInfo))
            {
                value = PersonalDict[worldInfo];
                return true;
            }
            else if (SharedDict.ContainsKey(worldInfo))
            {
                value = SharedDict[worldInfo];
                return true;
            }
            else if (GodDict.ContainsKey(worldInfo))
            {
                value = GodDict[worldInfo];
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}
