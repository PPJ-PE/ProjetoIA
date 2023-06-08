using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetoIA
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float linearSpeed;

        private void SetXZPos(Vector3 newPos)
        {
            transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
        }
        public void Move(Vector2 moveDir)
        {
            Vector3 pos = transform.position;
            pos.x += moveDir.x * linearSpeed * Time.deltaTime;
            pos.z += moveDir.y * linearSpeed * Time.deltaTime;
            SetXZPos(pos);
        }

    }
}
