using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadaPosiçao : MonoBehaviour
{
    [SerializeField]
    Transform granada, alvo;

    float distancia;
    float tempoTotal;
    float tempoParcial;
    Vector3 posIniGranada;
    Vector3 vetorVelocidade;

    // Start is called before the first frame update
    void Start()
    {
        distancia = Vector3.Distance(granada.position, alvo.position);
        float velocidade = Mathf.Sqrt(distancia * 9.81f / Mathf.Sin(90));
        tempoTotal = distancia / velocidade;
        vetorVelocidade = new Vector3(0, Mathf.Sin(45) * velocidade, Mathf.Cos(45) * velocidade);
        Debug.Log("tempo: " + tempoTotal);
        Debug.Log("distancia;" + distancia);
        Debug.Log("vetor velocidade:" + vetorVelocidade);
        tempoParcial = 0;
        posIniGranada = granada.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            granada.transform.Translate(0, vetorVelocidade.y * Time.deltaTime, vetorVelocidade.z * Time.deltaTime);// = posIniGranada + new Vector3(0, 0, vetorVelocidade.z * Time.deltaTime);
            vetorVelocidade.y -= 9.81f * Time.deltaTime;
            tempoParcial += Time.deltaTime;
            Debug.Log(vetorVelocidade.z * Time.deltaTime);
        }


    }
}



//A bola representa o local onde o quadrado cairá quando for pressionado.
//O Botão ainda não foi colocado, consegui fazer ele funcionar direito ainda