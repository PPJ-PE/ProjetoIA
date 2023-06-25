using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausMan : MonoBehaviour
{
    public Canvas pause;

    private void Start()
    {
      
    }

    public void Update()
    {

        //Precisa ser includo codigo que pausa o jogo
        //if (pause.enabled = false && Input.GetKeyDown(KeyCode.Backspace))
        {
            pause.enabled = true;
        }

        //else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            pause.enabled = false;
        }
    }

    public void Resumir()
    {
        //Incluir aqui codigo que despausa o jogo
        pause.enabled = false;
    }

    public void Sair()
    {
        SceneManager.LoadScene("Start");
    }


}
