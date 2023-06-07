using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProjetoIA
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] Camera mainCamera;
        [SerializeField] LayerMask inputRayCastMask;


        private void Update()
        {
            CheckInputs();
        }

        private void CheckInputs()
        {
            //TODO - Adicionar um button de ui que trigga essa chamada
            if (Input.GetMouseButtonDown(0)) MouseClick();
        }

        private void MouseClick()
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, float.MaxValue, inputRayCastMask))
            {
                Debug.Log("Controller sending waypoint to player");
                player.GoToPosition(hit.point);
            }
        }
    }
}
