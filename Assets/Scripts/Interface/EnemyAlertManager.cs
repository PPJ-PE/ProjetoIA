using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ProjetoIA
{
    public class EnemyAlertManager : MonoBehaviour
    {
        [Header("Object Referencing")]
        [SerializeField]
        private Image alertLevelGaugeFill;

        [Space(10)]

        [Header("Object Manipulation")]
        [SerializeField]
        private float fillSpeed;
        [SerializeField]
        private float coloringSpeed;
        [SerializeField]
        private Color[] fillColor;

        [Space(10)]

        [Header("Logistics")]
        [Range(0, 1)]
        [SerializeField]
        private float alertLevel;

        // Start is called before the first frame update
        void Start()
        {
            alertLevel = GetComponentInParent<IAlert>().GetAlertLevel();
            alertLevelGaugeFill = transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Image>();
        }

        private void Update()
        {
            alertLevel = GetComponentInParent<IAlert>().GetAlertLevel();

            alertLevelGaugeFill.fillAmount = Mathf.Lerp(alertLevel, GetComponentInParent<IAlert>().GetAlertLevel(), fillSpeed * Time.deltaTime);

            if (alertLevel >= 0.656f)
            {
                alertLevelGaugeFill.color = Color.Lerp(alertLevelGaugeFill.color, fillColor[2], coloringSpeed * Time.deltaTime);
            }
            else if (alertLevel >= 0.31f && alertLevel < 0.656f)
            {
                alertLevelGaugeFill.color = Color.Lerp(alertLevelGaugeFill.color, fillColor[1], coloringSpeed * Time.deltaTime);
            }
            else if (alertLevel < 0.31f)
            {
                alertLevelGaugeFill.color = Color.Lerp(alertLevelGaugeFill.color, fillColor[0], coloringSpeed * Time.deltaTime);
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }
}

