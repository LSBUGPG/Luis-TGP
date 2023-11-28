using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject TutorialImage;
    public AudioSource PlaySource;
    public AudioSource QuitSource;



    public void Start()
    {
        TutorialImage.SetActive(false);
    }

    public void PlayGame()
    {
        PlaySource.Play();
        StartCoroutine(Play());
    }     

    IEnumerator Play()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame()
    {
        QuitSource.Play();
        StartCoroutine(Quit());
    }

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
        Debug.Log("Im closing");
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
   
    }

}
