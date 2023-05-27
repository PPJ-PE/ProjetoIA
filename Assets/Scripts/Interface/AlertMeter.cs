using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class AlertMeter : MonoBehaviour
{
    [SerializeField]
    private float fillSpeed = 0.5f;

    private GameObject alertMeterNormal;
    private GameObject alertMeterActivated;
    private GameObject aMNBase;
    private GameObject aMNFill;
    private GameObject aMABase;
    private GameObject aMAFill;

    private Image aMNFillImage;
    private Image aMAFillImage;

    private float aMNFillAmount = 0;
    private float aMAFillAmount = 0;

    private enum AlertState
    {
        Normal,
        Activated
    }

    private AlertState currentState;

    // Start is called before the first frame update
    void Start()
    {
        alertMeterNormal = GetChildGameObject(gameObject, "AlertMeterNormal");
        alertMeterActivated = GetChildGameObject(gameObject, "AlertMeterActivated");
        aMNBase = GetChildGameObject(gameObject, "AMNBase");
        aMNFill = GetChildGameObject(gameObject, "AMNFill");
        aMABase = GetChildGameObject(gameObject, "AMABase");
        aMAFill = GetChildGameObject(gameObject, "AMAFill");

        aMNFillImage = aMNFill.GetComponent<Image>();
        aMAFillImage = aMAFill.GetComponent<Image>();

        currentState = AlertState.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        // debug
        if (Input.GetKeyDown(KeyCode.Z))
        {
            aMNFillAmount += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            aMNFillAmount -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            aMAFillAmount += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            aMAFillAmount -= 0.1f;
        }

        if (currentState == AlertState.Normal)
        {
            //aMNFillAmount = (verifica qual inimigo tem o maior alerta)

            // Setar o valor de destino do lerp para o maior valor de alerta de qualquer inimigo do mapa
            aMNFillImage.fillAmount = Mathf.Lerp(aMNFillImage.fillAmount, aMNFillAmount, fillSpeed);
        } else
        {
            //aMAFillAmount = (verifica qual inimigo tem o maior alerta)

            // Setar o valor de destino do lerp para o maior valor de alerta de qualquer inimigo do mapa
            aMAFillImage.fillAmount = Mathf.Lerp(aMAFillImage.fillAmount, aMAFillAmount, fillSpeed);
        }
        
        if (currentState == AlertState.Normal && aMNFillAmount >= 1)
        {
            ChangeAlertState(AlertState.Activated);
        }
        if (currentState == AlertState.Activated && aMAFillAmount <= 0)
        {
            ChangeAlertState(AlertState.Normal);
        }
    }

    private void ChangeAlertState(AlertState alertState)
    {
        currentState = alertState;
        if (alertState == AlertState.Normal)
        {
            aMNFillAmount = 0;
            alertMeterActivated.SetActive(false);
            alertMeterNormal.SetActive(true);
        }
        else
        {
            aMAFillAmount = 1;
            alertMeterActivated.SetActive(true);
            alertMeterNormal.SetActive(false);
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
