using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace ProjetoAI
{
    public class TitleScreen : MonoBehaviour
    {
        [Header("Object Referencing")]
        [SerializeField]
        private RectTransform mainGroup;
        [SerializeField]
        private RectTransform optionsGroup;
        [SerializeField]
        private RectTransform creditsGroup;
        [SerializeField]
        private CanvasGroup mainGroupCanvasGroup;
        [SerializeField]
        private CanvasGroup optionsGroupCanvasGroup;
        [SerializeField]
        private CanvasGroup creditsGroupCanvasGroup;
        [SerializeField]
        private AudioMixer mixer;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip buttonMove;
        [SerializeField]
        private AudioClip buttonConfirm;

        [Space(10)]

        [SerializeField]
        private TextMeshProUGUI fullscreenText;
        [SerializeField]
        private TextMeshProUGUI resolutionText;
        [SerializeField]
        private TextMeshProUGUI musicText;
        [SerializeField]
        private TextMeshProUGUI soundText;

        [Space(10)]

        [SerializeField]
        private RectTransform[] selections;
        [SerializeField]
        private Image[] selectionsImages;
        [SerializeField]
        private TextMeshProUGUI timeText;

        [Space(10)]

        [Header("Object Manipulation")]
        [SerializeField]
        private float positioningSpeed = 0.5f;
        [SerializeField]
        private float coloringSpeed = 0.5f;
        [SerializeField]
        private float transitionSpeed = 0.5f;

        [Space(10)]

        [Header("Logistics")]
        [SerializeField]
        private bool mousePress = false;
        [SerializeField]
        private int mainGroupSelectionIndex = 0;
        [SerializeField]
        private int optionsGroupSelectionIndex = 0;
        [SerializeField]
        private bool mainGroupInteract = true;
        [SerializeField]
        private bool optionsGroupInteract = false;
        [SerializeField]
        private bool creditsGroupInteract = false;

        [SerializeField]
        private int fullscreenValue = 0;
        [SerializeField]
        private int resolutionValue = 0;
        [SerializeField]
        private float bgmValue = 0;
        [SerializeField]
        private float sfxValue = 0;

        [SerializeField]
        private string[] fullScreenNames = { "Windowed", "Borderless Fullscreen", "Exclusive Fullscreen" };

        // Não é possivel serializar arrays nem listas multi-dimensionais
        private int[,] resolutions = {
            { 640, 360 },
            { 960, 540 },
            { 1280, 720 },
            { 1366, 768 },
            { 1600, 900 },
            { 1920, 1080 }
        };

        [SerializeField]
        private FullScreenMode[] fullScreenModes = {
            FullScreenMode.Windowed,
            FullScreenMode.FullScreenWindow,
            FullScreenMode.ExclusiveFullScreen
        };

        [SerializeField]
        private Coroutine fadeInCoroutine;
        [SerializeField]
        private Coroutine fadeOutCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1f;

            fullscreenValue = PlayerPrefs.GetInt("FULLSCREEN_MODE", 0);
            resolutionValue = PlayerPrefs.GetInt("RESOLUTION_SIZE", 1);
            bgmValue = PlayerPrefs.GetFloat("BGM_VOLUME", 0.05f);
            sfxValue = PlayerPrefs.GetFloat("SFX_VOLUME", 0.05f);

            Screen.SetResolution(resolutions[resolutionValue, 0], resolutions[resolutionValue, 1], fullScreenModes[fullscreenValue]);

            mixer.SetFloat("BGMParam", Mathf.Log10(PlayerPrefs.GetFloat("BGM_VOLUME", 1)) * 20);
            mixer.SetFloat("SFXParam", Mathf.Log10(PlayerPrefs.GetFloat("SFX_VOLUME", 1)) * 20);

            fullscreenText.text = fullScreenNames[fullscreenValue];
            resolutionText.text = resolutions[resolutionValue, 0] + " x " + resolutions[resolutionValue, 1];
            musicText.text = (int)(bgmValue * 100) + "%";
            soundText.text = (int)(sfxValue * 100) + "%";

            fadeOutCoroutine = StartCoroutine(EmptyCoroutine());
            fadeInCoroutine = StartCoroutine(EmptyCoroutine());
        }

        // Update is called once per frame
        void Update()
        {
            timeText.text = DateTime.Now.Hour + ":" + DateTime.Now.Minute;

            #region Menu Principal
            if (mainGroupInteract == true)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (mainGroupSelectionIndex == i)
                    {
                        selections[i].anchoredPosition = Vector2.Lerp(selections[i].anchoredPosition, new Vector2(-660, selections[i].anchoredPosition.y), Time.deltaTime * positioningSpeed);
                        selectionsImages[i].color = Color.Lerp(selectionsImages[i].color, new Color(1, 1, 1, 1), Time.deltaTime * coloringSpeed);
                    }
                    else
                    {
                        selections[i].anchoredPosition = Vector2.Lerp(selections[i].anchoredPosition, new Vector2(-760, selections[i].anchoredPosition.y), Time.deltaTime * positioningSpeed);
                        selectionsImages[i].color = Color.Lerp(selectionsImages[i].color, new Color(0.8f, 0.8f, 0.8f, 0.75f), Time.deltaTime * coloringSpeed);
                    }
                }

                // Movimentar nas seleções
                if (Input.GetButtonDown("Vertical"))
                {
                    audioSource.PlayOneShot(buttonMove);
                    mainGroupSelectionIndex = (int)Wrap(mainGroupSelectionIndex - (int)Input.GetAxisRaw("Vertical"), 0, 4);
                }

                // Confirmar a seleção
                if (Input.GetButtonDown("Submit") || mousePress)
                {
                    audioSource.PlayOneShot(buttonConfirm);
                    for (int i = 0; i < 4; i++)
                    {
                        if (mainGroupSelectionIndex == i)
                        {
                            selectionsImages[i].color = new Color(0.8f, 0.45f, 0.185f, 1);
                        }
                    }
                    switch (mainGroupSelectionIndex)
                    {
                        case 0:

                            StopCoroutine(fadeOutCoroutine);
                            fadeOutCoroutine = StartCoroutine(FadeOutGroup(mainGroup, mainGroupCanvasGroup, mainGroupInteract, (value) => mainGroupInteract = value));
                            StartCoroutine(ScreenTransition.instance.GoToScene("LevelDesign"));
                            break;

                        case 1:

                            StopCoroutine(fadeOutCoroutine);
                            optionsGroupSelectionIndex = 0;
                            fadeOutCoroutine = StartCoroutine(FadeOutGroup(mainGroup, mainGroupCanvasGroup, mainGroupInteract, (value) => mainGroupInteract = value));
                            fadeInCoroutine = StartCoroutine(FadeInGroup(optionsGroup, optionsGroupCanvasGroup, optionsGroupInteract, (value) => optionsGroupInteract = value));
                            break;

                        case 2:

                            StopCoroutine(fadeOutCoroutine);
                            fadeOutCoroutine = StartCoroutine(FadeOutGroup(mainGroup, mainGroupCanvasGroup, mainGroupInteract, (value) => mainGroupInteract = value));
                            fadeInCoroutine = StartCoroutine(FadeInGroup(creditsGroup, creditsGroupCanvasGroup, creditsGroupInteract, (value) => creditsGroupInteract = value));
                            break;

                        case 3:

                            StopCoroutine(fadeOutCoroutine);
                            fadeOutCoroutine = StartCoroutine(FadeOutGroup(mainGroup, mainGroupCanvasGroup, mainGroupInteract, (value) => mainGroupInteract = value));
                            Application.Quit();
                            break;
                    }
                    mousePress = false;

                }

            }
            #endregion

            #region Menu Options
            if (optionsGroupInteract == true)
            {
                if (Input.GetButtonDown("Vertical"))
                {
                    audioSource.PlayOneShot(buttonMove);
                    optionsGroupSelectionIndex = (int)Wrap(optionsGroupSelectionIndex - (int)Input.GetAxisRaw("Vertical"), 0, 6);
                }

                if (Input.GetButtonDown("Horizontal"))
                {
                    audioSource.PlayOneShot(buttonMove);
                    switch (optionsGroupSelectionIndex)
                    {
                        case 0: // Fullscreen

                            fullscreenValue += (int)Input.GetAxisRaw("Horizontal");
                            fullscreenText.text = fullScreenNames[fullscreenValue];
                            break;

                        case 1: // Resolution

                            resolutionValue += (int)Input.GetAxisRaw("Horizontal");
                            resolutionText.text = resolutions[resolutionValue, 0] + " x " + resolutions[resolutionValue, 1];
                            break;

                        case 2: // Music Volume

                            bgmValue += (int)Input.GetAxisRaw("Horizontal") * 0.1f;
                            musicText.text = (int)(bgmValue * 100) + "%";
                            break;

                        case 3: // Sound Volume

                            sfxValue += (int)Input.GetAxisRaw("Horizontal") * 0.1f;
                            soundText.text = (int)(sfxValue * 100) + "%";
                            break;

                    }
                }

                if (Input.GetButtonDown("Submit") || mousePress)
                {
                    audioSource.PlayOneShot(buttonConfirm);
                    switch (optionsGroupSelectionIndex)
                    {
                        case 4:

                            Screen.SetResolution(resolutions[resolutionValue, 0], resolutions[resolutionValue, 1], fullScreenModes[fullscreenValue]);

                            PlayerPrefs.SetInt("RESOLUTION_SIZE", resolutionValue);
                            PlayerPrefs.SetInt("FULLSCREEN_MODE", fullscreenValue);

                            PlayerPrefs.SetFloat("BGM_VOLUME", bgmValue);
                            PlayerPrefs.SetFloat("SFX_VOLUME", sfxValue);

                            mixer.SetFloat("BGMParam", Mathf.Log10(PlayerPrefs.GetFloat("BGM_VOLUME", 1)) * 20);
                            mixer.SetFloat("SFXParam", Mathf.Log10(PlayerPrefs.GetFloat("SFX_VOLUME", 1)) * 20);

                            StopCoroutine(fadeOutCoroutine);
                            fadeOutCoroutine = StartCoroutine(FadeOutGroup(optionsGroup, optionsGroupCanvasGroup, optionsGroupInteract, (value) => optionsGroupInteract = value));
                            fadeInCoroutine = StartCoroutine(FadeInGroup(mainGroup, mainGroupCanvasGroup, mainGroupInteract, (value) => mainGroupInteract = value));
                            break;

                    }
                    mousePress = false;
                }

                if (Input.GetButtonDown("Cancel"))
                {
                    audioSource.PlayOneShot(buttonMove);
                    StopCoroutine(fadeOutCoroutine);
                    fadeOutCoroutine = StartCoroutine(FadeOutGroup(optionsGroup, optionsGroupCanvasGroup, optionsGroupInteract, (value) => optionsGroupInteract = value));
                    fadeInCoroutine = StartCoroutine(FadeInGroup(mainGroup, mainGroupCanvasGroup, mainGroupInteract, (value) => mainGroupInteract = value));
                }

            }
            #endregion

            if (creditsGroupInteract == true)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    audioSource.PlayOneShot(buttonMove);
                    StopCoroutine(fadeOutCoroutine);
                    fadeOutCoroutine = StartCoroutine(FadeOutGroup(creditsGroup, creditsGroupCanvasGroup, creditsGroupInteract, (value) => creditsGroupInteract = value));
                    fadeInCoroutine = StartCoroutine(FadeInGroup(mainGroup, mainGroupCanvasGroup, mainGroupInteract, (value) => mainGroupInteract = value));
                }
            }
        }

        private IEnumerator EmptyCoroutine()
        {
            yield return null;
        }

        private IEnumerator FadeInGroup(Transform group, CanvasGroup canvasGroup, bool groupInteract, System.Action<bool> setBool)
        {
            setBool(!groupInteract);
            while (group.localScale.x != 1)
            {
                group.localScale = Vector2.Lerp(group.localScale, new Vector2(1, 1), Time.deltaTime * transitionSpeed);
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, Time.deltaTime * transitionSpeed * 2);
                yield return null;
            }
        }

        private IEnumerator FadeOutGroup(Transform group, CanvasGroup canvasGroup, bool groupInteract, System.Action<bool> setBool)
        {
            setBool(!groupInteract);
            if (fadeInCoroutine != null)
            {
                StopCoroutine(fadeInCoroutine);
            }
            while (group.localScale.x != 0)
            {
                group.localScale = Vector2.Lerp(group.localScale, new Vector2(0, 0), Time.deltaTime * transitionSpeed);
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, Time.deltaTime * transitionSpeed * 2);
                yield return null;
            }
        }

        private float Wrap(float _val, float _min, float _max)
        {
            _val = _val - (float)Mathf.Round((_val - _min) / (_max - _min)) * (_max - _min);
            if (_val < 0)
                _val = _val + _max - _min;
            return _val;
        }
    }
}

