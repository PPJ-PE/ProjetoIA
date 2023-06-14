using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA
{
    public class ThrowSystem : MonoBehaviour
    {
        [SerializeField] private Transform throwableItemParent;
        [SerializeField] private ThrowableItem throwableItemPrefab;

        public void ThrowItem(Vector3 target)
        {
            ThrowableItem tItem = Instantiate(throwableItemPrefab, transform.position, Quaternion.identity, throwableItemParent);

            //tItem.ApplyForce(/*calcular quanto colocar aqui*/);
        }
    }
}
