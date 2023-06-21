using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ProjetoIA
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] Camera mainCamera;
        [SerializeField] CameraController mainCameraCtrl;
        [SerializeField] LayerMask inputRayCastMask;
        

        private static InputManager instance;

        private void Awake()
        {
            if (instance)
            {
                Debug.LogError("Mais de uma instancia detectada!");
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        private void Update()
        {
            CheckInputs();
        }

        private void CheckInputs()
        {
            Vector3 normalizedMousePos;
            Vector3 inputAxis;

            //TODO - Adicionar um button de ui que trigga essa chamada
            if (Input.GetMouseButtonDown(0)) MouseClick();

            //Move cam
            inputAxis = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f) * 0.25f;
            normalizedMousePos = mainCamera.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f, 0.5f, 0.0f);
            if (Mathf.Abs(normalizedMousePos.x) > 0.45f || Mathf.Abs(normalizedMousePos.y) > 0.45f)
            {
                if (Mathf.Abs(normalizedMousePos.x) > Mathf.Abs(normalizedMousePos.y))
                    normalizedMousePos = normalizedMousePos.normalized * 
                                            (Mathf.Abs(normalizedMousePos.x) - 0.45f) * 
                                            20.0f;
                else
                    normalizedMousePos = normalizedMousePos.normalized * (Mathf.Abs(normalizedMousePos.y) - 0.45f) * 20.0f;
                MoveCamera(normalizedMousePos);
            }
            else if(inputAxis.magnitude > 0.005f)
            {
                MoveCamera(inputAxis);
            }
        }
        private void MoveCamera(Vector3 mousePos)
        {
            mainCameraCtrl.Move(mousePos.normalized);
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
