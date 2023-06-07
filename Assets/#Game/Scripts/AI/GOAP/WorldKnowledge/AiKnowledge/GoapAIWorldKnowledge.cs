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
        public void UpdatePersonalKnowledge(IReadOnlyWorldKnowledge worldKnowledge) => 
            personalKnowledge.UpdateWorldKnowledge(worldKnowledge);
        public void UpdatePersonalKnowledge(bWorldInfo bWorldInfo, bool value) =>
            personalKnowledge.UpdateWorldKnowledge(bWorldInfo, value);
        public void UpdatePersonalKnowledge(fWorldInfo fWorldInfo, float value) =>
            personalKnowledge.UpdateWorldKnowledge(fWorldInfo, value);
        public void UpdatePersonalKnowledge(eWorldInfo eWorldInfo, int value) =>
            personalKnowledge.UpdateWorldKnowledge(eWorldInfo, value);
        public void UpdateSharedKnowledge(IReadOnlyWorldKnowledge worldKnowledge) =>
            sharedKnowledge.UpdateWorldKnowledge(worldKnowledge);
        public void UpdateSharedKnowledge(bWorldInfo bWorldInfo, bool value) =>
            sharedKnowledge.UpdateWorldKnowledge(bWorldInfo, value);
        public void UpdateSharedKnowledge(fWorldInfo fWorldInfo, float value) =>
            sharedKnowledge.UpdateWorldKnowledge(fWorldInfo, value);
        public void UpdateSharedKnowledge(eWorldInfo eWorldInfo, int value) =>
            sharedKnowledge.UpdateWorldKnowledge(eWorldInfo, value);
        public void UpdateGodKnowledge(IReadOnlyWorldKnowledge worldKnowledge) =>
            godKnowledge.UpdateWorldKnowledge(worldKnowledge);
        public void UpdateGodKnowledge(bWorldInfo bWorldInfo, bool value) =>
            godKnowledge.UpdateWorldKnowledge(bWorldInfo, value);
        public void UpdateGodKnowledge(fWorldInfo fWorldInfo, float value) =>
            godKnowledge.UpdateWorldKnowledge(fWorldInfo, value);
        public void UpdateGodKnowledge(eWorldInfo eWorldInfo, int value) =>
            godKnowledge.UpdateWorldKnowledge(eWorldInfo, value);
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
