using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA
{
    [ExecuteInEditMode]
    public class ReplaceObjs : MonoBehaviour
    {
        [SerializeField] private Transform objsParent;
        [SerializeField] private GameObject replacementPrefab;
        [SerializeField] private bool button = false;

        private void Update()
        {
            if (button)
            { 
                button = false;
                OnButtonClick();
            }
        }
        private void OnButtonClick()
        {
            Transform[] children = new Transform[objsParent.childCount];
            GameObject[] replacements = new GameObject[objsParent.childCount];
            Transform objToReplace = null;

            for (int i = 0; i < children.Length; i++)
            {
                children[i] = objsParent.GetChild(i);
            }
            for (int i = 0; i < replacements.Length; i++)
            {
                objToReplace = children[i];
                replacements[i] = Instantiate(replacementPrefab, objToReplace.position, objToReplace.rotation, objsParent);
                replacements[i].transform.localScale = objToReplace.localScale;
            }
            for (int i = 0; i < children.Length; i++)
            {
                DestroyImmediate(children[i].gameObject);
            }
        }
    }
}
