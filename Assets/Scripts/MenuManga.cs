using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MenuManga : MonoBehaviour
{
    public VideoPlayer video;
    public RawImage flicker;


    public Animator animator;

    public void Start()
    {
        flicker.enabled = false;
    }
    public void StartGame()
    {
        flicker.enabled = true;
        video.Play();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
    }

    public void Transition()
    {
        Debug.Log("HI");
    }
}
