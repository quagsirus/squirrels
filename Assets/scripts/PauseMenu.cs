using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseMenuUI;
    AudioSource bgm;
    FxPlayer fxPlayer;
    public Slider fxVolume, musicVolume;

    private void Start()
    {
        fxVolume.value = PlayerPrefs.GetFloat("fxVolume", 0.6f);
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume", 0.05f) * 3;
        fxVolume.onValueChanged.AddListener(FxVolumeChanged);
        musicVolume.onValueChanged.AddListener(MusicVolumeChanged);

        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        fxPlayer = GameObject.Find("SFX").GetComponent<FxPlayer>();
    }

    void FxVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("fxVolume", value);
        fxPlayer.UpdateVolume();
    }

    void MusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value / 3);
        bgm.volume = value / 3;
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
