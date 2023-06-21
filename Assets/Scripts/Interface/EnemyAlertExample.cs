using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA
{
    public class EnemyAlertExample : MonoBehaviour, IAlert
    {
        private float alertLevel;

        private void Update()
        {
            if (gameObject.name == "Capsule")
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    alertLevel += 0.1f;
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    alertLevel -= 0.1f;
                }
            }
            

            if (gameObject.name == "Capsule (1)")
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    alertLevel += 0.1f;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    alertLevel -= 0.1f;
                }
            }
        }

        public float GetAlertLevel()
        {
            return alertLevel;
        }
    }
}

