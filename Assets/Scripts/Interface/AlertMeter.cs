using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

namespace ProjetoIA
{
    public class AlertMeter : MonoBehaviour
    {
        [Header("Object Referencing")]

        [SerializeField]
        private GameObject alertMeter;

        [SerializeField]
        private RectTransform alertMeterRect;

        [SerializeField]
        private GameObject radioBase;

        [SerializeField]
        private GameObject radioFill;

        [SerializeField]
        private GameObject radioShine;

        [SerializeField]
        private RectTransform radioShineRect;

        [SerializeField]
        private GameObject radioShadow;

        [SerializeField]
        private CanvasGroup radioShadowCGroup;

        [SerializeField]
        private Image fillImage;

        [SerializeField]
        private GameObject[] enemies;

        [Space(10)]

        [Header("Object Manipulation")]

        [SerializeField]
        private Sprite[] fillSprites; // 0 é normal, 1 é meio termo e 3 é alerta máximo

        [Space(10)]

        [Header("Logistics")]

        [SerializeField]
        private int fillSpriteID = 2;

        [SerializeField]
        private float fillAmount = 0;

        [SerializeField]
        private float fillSpeed = 0.5f;

        private enum AlertState
        {
            Normal,
            Activated
        }

        private AlertState currentState;

        // Start is called before the first frame update
        void Start()
        {
            currentState = AlertState.Normal;
            radioShine.SetActive(false);

            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            float alertLevelAmountHighest = 0;
            foreach (GameObject enemy in enemies)
            {
                var alertLevelAmount = enemy.GetComponent<IAlert>().GetAlertLevel();
                if (alertLevelAmount > alertLevelAmountHighest)
                {
                    alertLevelAmountHighest = alertLevelAmount;
                    fillAmount = alertLevelAmount;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            #region DEBUG
            if (Input.GetKeyDown(KeyCode.Z))
            {
                fillAmount += 0.1f;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                fillAmount -= 0.1f;
            }
            #endregion

            float alertLevelAmountHighest = 0;
            foreach (GameObject enemy in enemies)
            {
                var alertLevelAmount = enemy.GetComponent<IAlert>().GetAlertLevel();
                if (alertLevelAmount > alertLevelAmountHighest)
                {
                    alertLevelAmountHighest = alertLevelAmount;
                    fillAmount = alertLevelAmount;
                }
            }

            // Altera a imagem do fill dependendo do quão cheio está
            if (fillAmount >= 0.656f && fillSpriteID != 2)
            {
                fillSpriteID = 2;
                fillImage.sprite = fillSprites[fillSpriteID];
            }
            else if (fillAmount >= 0.31f && fillAmount < 0.656f && fillSpriteID != 1)
            {
                fillSpriteID = 1;
                fillImage.sprite = fillSprites[fillSpriteID];
            }
            else if (fillAmount < 0.31f && fillSpriteID != 0)
            {
                fillSpriteID = 0;
                fillImage.sprite = fillSprites[fillSpriteID];
            }

            if (fillAmount >= 0.656f)
            {
                alertMeterRect.localRotation = Quaternion.Euler(new Vector3(alertMeterRect.localRotation.x, alertMeterRect.localRotation.y, Random.Range(-1, 1)));
            }

            if (fillAmount >= 0.656f)
            {
                radioShadowCGroup.alpha = Mathf.Lerp(radioShadowCGroup.alpha, 1, 2f * Time.deltaTime);
            }
            else
            {
                radioShadowCGroup.alpha = Mathf.Lerp(radioShadowCGroup.alpha, 0, 2f * Time.deltaTime);
            }

            // Manipula a quantidade de fill
            fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, fillAmount, fillSpeed);

            // Executa a ação de trocar de estados
            if (currentState == AlertState.Normal && fillAmount >= 1)
            {
                ChangeAlertState(AlertState.Activated);
            }
            if (currentState == AlertState.Activated && fillAmount <= 0)
            {
                ChangeAlertState(AlertState.Normal);
            }
        }

        private void ChangeAlertState(AlertState alertState)
        {
            currentState = alertState;
            if (alertState == AlertState.Normal)
            {
                fillAmount = 0;
            }
            else
            {
                StartCoroutine(ShakeRadio());
                StartCoroutine(InflateRadio());
            }
        }

        private IEnumerator ShakeRadio()
        {
            while (currentState == AlertState.Activated)
            {
                radioShineRect.localRotation *= Quaternion.Euler(Vector3.forward * 600 * Time.deltaTime);
                alertMeterRect.localRotation = Quaternion.Euler(new Vector3(alertMeterRect.localRotation.x, alertMeterRect.localRotation.y, Random.Range(-4, 4)));
                yield return null;
            }
            radioShineRect.localRotation = Quaternion.Euler(new Vector3(radioShineRect.localRotation.x, radioShineRect.localRotation.y, 0));
            alertMeterRect.localRotation = Quaternion.Euler(new Vector3(radioShineRect.localRotation.x, radioShineRect.localRotation.y, 0));
            radioShine.SetActive(false);
        }

        private IEnumerator InflateRadio()
        {
            // Define o tempo que será tomado em cada etapa da animação
            float timeStart = 0.1f;
            float timeHold = 0f;
            float timeEnd = 0.2f;
            Vector3 initScale = alertMeterRect.localScale;

            for (float i = 0; i < timeStart; i += 0)
            {
                float scale = i / timeStart;
                scale = scale * scale * scale;
                alertMeterRect.localScale = initScale * Mathf.Lerp(1, 1.3f, scale);
                i += Time.deltaTime;
                yield return null;
            }
            radioShine.SetActive(true);
            yield return new WaitForSeconds(timeHold);
            for (float i = timeEnd; i > 0; i += 0)
            {
                float scale = i / timeEnd;
                scale = scale * scale * scale;
                alertMeterRect.localScale = initScale * Mathf.Lerp(1, 1.3f, scale);
                i -= Time.deltaTime;
                yield return null;
            }
        }

        public void SetAlertLevel(float _alertLevel)
        {
            if (_alertLevel > 1.0f || _alertLevel < 0)
            {
                Debug.LogError("Invalid alert level!!!");
                return;
            }
            fillAmount = _alertLevel;
        }
    }
}
