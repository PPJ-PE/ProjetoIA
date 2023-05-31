using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ProjetoIA
{
    public class AlertMeter : MonoBehaviour
    {
        [SerializeField]
        private float fillSpeed = 0.5f;

        private GameObject alertMeter;
        private RectTransform alertMeterRect;

        private GameObject radioBase;

        private GameObject radioFill;

        private GameObject radioShine;
        private RectTransform radioShineRect;

        private GameObject radioShadow;
        private CanvasGroup radioShadowCGroup;

        private Image fillImage;

        [SerializeField]
        private Sprite[] fillSprites; // 0 é normal, 1 é meio termo e 3 é alerta máximo
        private int fillSpriteID = 0;

        private float fillAmount = 0;

        private enum AlertState
        {
            Normal,
            Activated
        }

        private AlertState currentState;

        // Start is called before the first frame update
        void Start()
        {
            alertMeter = GetChildGameObject(gameObject, "AlertMeter");
            alertMeterRect = alertMeter.GetComponent<RectTransform>();

            radioBase = GetChildGameObject(gameObject, "Base");
            radioFill = GetChildGameObject(gameObject, "Fill");

            radioShine = GetChildGameObject(gameObject, "Shine");
            radioShineRect = radioShine.GetComponent<RectTransform>();

            radioShadow = GetChildGameObject(gameObject, "Shadow");
            radioShadowCGroup = radioShadow.GetComponent<CanvasGroup>();

            fillImage = radioFill.GetComponent<Image>();

            currentState = AlertState.Normal;

            radioShine.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            // debug
            if (Input.GetKeyDown(KeyCode.Z))
            {
                fillAmount += 0.1f;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                fillAmount -= 0.1f;
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

        private GameObject GetChildGameObject(GameObject fromGameObject, string withName)
        {
            // Author: Isaac Dart, June-13.
            Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
            return null;
        }
    }
}
