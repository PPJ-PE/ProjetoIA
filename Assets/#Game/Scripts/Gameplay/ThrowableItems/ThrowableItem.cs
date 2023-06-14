using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA
{
    public class ThrowableItem : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;

        public void ApplyForce(Vector3 force)
        {
            rb.AddForce(force); //Talvez tenha que mudar
        }
    }
}