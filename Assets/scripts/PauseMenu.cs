using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseMenuUI;
    AudioSource bgm;

    private void Start()
    {
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) 
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        bgm.mute = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        bgm.mute = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Main");
    }

    public void Quit() 
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}
