using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjetoAI
{
    public class ScreenTransition : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        public static ScreenTransition instance;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator GoToScene(string sceneName, bool restartTime = false)
        {
            animator.SetTrigger("SwitchScene");
            yield return new WaitForSecondsRealtime(1f);
            if (restartTime == true)
            {
                Time.timeScale = 1f;
            }
            SceneManager.LoadScene(sceneName);
            if (sceneName == "MainMenu")
            {
                Destroy(gameObject);
            }
        }
    }
}

