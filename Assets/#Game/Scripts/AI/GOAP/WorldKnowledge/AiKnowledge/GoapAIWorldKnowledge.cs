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

        protected IReadOnlyWorldKnowledge PersonalDict { get { return personalKnowledge.GetWorldKnowledge(); } }
        protected IReadOnlyWorldKnowledge SharedDict { get { return sharedKnowledge.GetWorldKnowledge(); } }
        protected IReadOnlyWorldKnowledge GodDict { get { return godKnowledge.GetWorldKnowledge(); } }

        public GoapAIWorldKnowledge(GoapAISharedKnowledge sharedKnowledge, GoapAIGodKnowledge godKnowledge)
        {
            personalKnowledge = new GoapAIPersonalKnowledge();
            this.sharedKnowledge = sharedKnowledge;
            this.godKnowledge = godKnowledge;
        }
        public bool GetWorldKnowledge(bWorldInfo bWorldInfo, out bool value)
        {
            if (PersonalDict.ContainsKey(bWorldInfo))
            {
                value = PersonalDict[bWorldInfo];
                return true;
            }
            else if (SharedDict.ContainsKey(bWorldInfo))
            {
                value = SharedDict[bWorldInfo];
                return true;
            }
            else if (GodDict.ContainsKey(bWorldInfo))
            {
                value = GodDict[bWorldInfo];
                return true;
            }
            else
            {
                value = false;
                return false;
            }
        }
        public bool GetWorldKnowledge(fWorldInfo fWorldInfo, out float value)
        {
            if (PersonalDict.ContainsKey(fWorldInfo))
            {
                value = PersonalDict[fWorldInfo];
                return true;
            }
            else if (SharedDict.ContainsKey(fWorldInfo))
            {
                value = SharedDict[fWorldInfo];
                return true;
            }
            else if (GodDict.ContainsKey(fWorldInfo))
            {
                value = GodDict[fWorldInfo];
                return true;
            }
            else
            {
                value = 0.0f;
                return false;
            }
        }
        public bool GetWorldKnowledge(eWorldInfo eWorldInfo, out int value)
        {
            if (PersonalDict.ContainsKey(eWorldInfo))
            {
                value = PersonalDict[eWorldInfo];
                return true;
            }
            else if (SharedDict.ContainsKey(eWorldInfo))
            {
                value = SharedDict[eWorldInfo];
                return true;
            }
            else if (GodDict.ContainsKey(eWorldInfo))
            {
                value = GodDict[eWorldInfo];
                return true;
            }
            else
            {
                value = 0;
                return false;
            }
        }
    }
}
