using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePause = false;
    public GameObject PauseUI;
    public GameObject OptionsUI;
    public GameObject LevelSelUI;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public GameObject soundManager;
    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePause = false;
        OptionsUI.SetActive(false);
        soundManager.SetActive(true);
        LevelSelUI.SetActive(false);
    }

    void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePause = true;
        soundManager.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void quitToDesktop()
    {
        Application.Quit();

    }
}


