using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace ProjetoIA.GOAP
{
    public class WorldKnowledge : IReadOnlyWorldKnowledge, IDictionary<bWorldInfo, bool>, IDictionary<fWorldInfo, float>, IDictionary<eWorldInfo, int>
    {
        private Dictionary<bWorldInfo, bool> bKnowledge;
        private Dictionary<fWorldInfo, float> fKnowledge;
        private Dictionary<eWorldInfo, int> eKnowledge;

        public WorldKnowledge()
        {
            bKnowledge = new Dictionary<bWorldInfo, bool>();
            fKnowledge = new Dictionary<fWorldInfo, float>();
            eKnowledge = new Dictionary<eWorldInfo, int>();
            
        }

        public bool this[bWorldInfo key]
        {
            get => bKnowledge[key];
            set => bKnowledge[key] = value;
        }

        public float this[fWorldInfo key]
        {
            get => fKnowledge[key];
            set => fKnowledge[key] = value;
        }

        public int this[eWorldInfo key]
        {
            get => eKnowledge[key];
            set => eKnowledge[key] = value;
        }
        

        public int Count => bKnowledge.Count + fKnowledge.Count + eKnowledge.Count;

        public bool IsReadOnly => false;

        public void Add(bWorldInfo key, bool value)
        {
            bKnowledge.Add(key, value);
        }

        public void Add(fWorldInfo key, float value)
        {
            fKnowledge.Add(key, value);
        }

        public void Add(eWorldInfo key, int value)
        {
            eKnowledge.Add(key, value);
        }


        public void Add(KeyValuePair<bWorldInfo, bool> item)
        {
            ((ICollection<KeyValuePair<bWorldInfo, bool>>)bKnowledge).Add(item);
        }

        public void Add(KeyValuePair<fWorldInfo, float> item)
        {
            ((ICollection<KeyValuePair<fWorldInfo, float>>)fKnowledge).Add(item);
        }

        public void Add(KeyValuePair<eWorldInfo, int> item)
        {
            ((ICollection<KeyValuePair<eWorldInfo, int>>)eKnowledge).Add(item);
        }

        public void Clear()
        {
            bKnowledge.Clear();
            fKnowledge.Clear();
            eKnowledge.Clear();
        }

        public bool Contains(KeyValuePair<bWorldInfo, bool> item)
        {
            return ((ICollection<KeyValuePair<bWorldInfo, bool>>)bKnowledge).Contains(item);
        }

        public bool Contains(KeyValuePair<fWorldInfo, float> item)
        {
            return ((ICollection<KeyValuePair<fWorldInfo, float>>)fKnowledge).Contains(item);
        }

        public bool Contains(KeyValuePair<eWorldInfo, int> item)
        {
            return ((ICollection<KeyValuePair<eWorldInfo, int>>)eKnowledge).Contains(item);
        }

        public bool ContainsKey(bWorldInfo key)
        {
            return bKnowledge.ContainsKey(key);
        }

        public bool ContainsKey(fWorldInfo key)
        {
            return fKnowledge.ContainsKey(key);
        }

        public bool ContainsKey(eWorldInfo key)
        {
            return eKnowledge.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<bWorldInfo, bool>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<bWorldInfo, bool>>)bKnowledge).CopyTo(array, arrayIndex);
        }

        public void CopyTo(KeyValuePair<fWorldInfo, float>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<fWorldInfo, float>>)fKnowledge).CopyTo(array, arrayIndex);
        }

        public void CopyTo(KeyValuePair<eWorldInfo, int>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<eWorldInfo, int>>)eKnowledge).CopyTo(array, arrayIndex);
        }

        public bool Remove(bWorldInfo key)
        {
            return bKnowledge.Remove(key);
        }

        public bool Remove(fWorldInfo key)
        {
            return fKnowledge.Remove(key);
        }

        public bool Remove(eWorldInfo key)
        {
            return eKnowledge.Remove(key);
        }

        public bool Remove(KeyValuePair<bWorldInfo, bool> item)
        {
            return ((ICollection<KeyValuePair<bWorldInfo, bool>>)bKnowledge).Remove(item);
        }

        public bool Remove(KeyValuePair<fWorldInfo, float> item)
        {
            return ((ICollection<KeyValuePair<fWorldInfo, float>>)fKnowledge).Remove(item);
        }

        public bool Remove(KeyValuePair<eWorldInfo, int> item)
        {
            return ((ICollection<KeyValuePair<eWorldInfo, int>>)eKnowledge).Remove(item);
        }

        public bool TryGetValue(bWorldInfo key, out bool value)
        {
            return bKnowledge.TryGetValue(key, out value);
        }

        public bool TryGetValue(fWorldInfo key, out float value)
        {
            return fKnowledge.TryGetValue(key, out value);
        }

        public bool TryGetValue(eWorldInfo key, out int value)
        {
            return eKnowledge.TryGetValue(key, out value);
        }


        ICollection<bWorldInfo> IDictionary<bWorldInfo, bool>.Keys => bKnowledge.Keys;
        ICollection<fWorldInfo> IDictionary<fWorldInfo, float>.Keys => fKnowledge.Keys;
        ICollection<eWorldInfo> IDictionary<eWorldInfo, int>.Keys => eKnowledge.Keys;
        ICollection<bool> IDictionary<bWorldInfo, bool>.Values => bKnowledge.Values;
        ICollection<float> IDictionary<fWorldInfo, float>.Values => fKnowledge.Values;
        ICollection<int> IDictionary<eWorldInfo, int>.Values => eKnowledge.Values;
        IEnumerable<bWorldInfo> IReadOnlyDictionary<bWorldInfo, bool>.Keys => bKnowledge.Keys;
        IEnumerable<fWorldInfo> IReadOnlyDictionary<fWorldInfo, float>.Keys => fKnowledge.Keys;
        IEnumerable<eWorldInfo> IReadOnlyDictionary<eWorldInfo, int>.Keys => eKnowledge.Keys;
        IEnumerable<bool> IReadOnlyDictionary<bWorldInfo, bool>.Values => bKnowledge.Values;
        IEnumerable<float> IReadOnlyDictionary<fWorldInfo, float>.Values => fKnowledge.Values;
        IEnumerable<int> IReadOnlyDictionary<eWorldInfo, int>.Values => eKnowledge.Values;
        IEnumerator IEnumerable.GetEnumerator()
        {
            List<object> enumerator = new List<object>(Count);

            foreach(bool b in bKnowledge.Values) enumerator.Add(b);
            foreach (float f in fKnowledge.Values) enumerator.Add(f);
            foreach (int e in eKnowledge.Values) enumerator.Add(e);

            return enumerator.GetEnumerator();
        }

        IEnumerator<KeyValuePair<bWorldInfo, bool>> IEnumerable<KeyValuePair<bWorldInfo, bool>>.GetEnumerator()
        {
            return bKnowledge.GetEnumerator();
        }

        IEnumerator<KeyValuePair<fWorldInfo, float>> IEnumerable<KeyValuePair<fWorldInfo, float>>.GetEnumerator()
        {
            return fKnowledge.GetEnumerator();
        }

        IEnumerator<KeyValuePair<eWorldInfo, int>> IEnumerable<KeyValuePair<eWorldInfo, int>>.GetEnumerator()
        {
            return eKnowledge.GetEnumerator();
        }

        public bool GetWorldKnowledge(bWorldInfo bWorldInfo, out bool value) {
            if (bKnowledge.ContainsKey(bWorldInfo)) 
            {
                value = bKnowledge[bWorldInfo];
                return true;
            }
            else 
            {
                value = false;
                return false;
            }
        }

        public bool GetWorldKnowledge(fWorldInfo fWorldInfo, out float value) {
            if (fKnowledge.ContainsKey(fWorldInfo)) 
            {
                value = fKnowledge[fWorldInfo];
                return true;
            }
            else 
            {
                value = 0f;
                return false;
            }
        }

        public bool GetWorldKnowledge(eWorldInfo eWorldInfo, out int value) {
            if (eKnowledge.ContainsKey(eWorldInfo)) {
                value = eKnowledge[eWorldInfo];
                return true;
            }
            else {
                value = 0;
                return false;
            }
        }
    }
}
