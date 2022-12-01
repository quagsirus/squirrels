using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public Camera m_Camera;
    public Button m_StartGame, m_OpenSettings, m_ReturnFromSettings, m_HowToPlay, m_ReturnFromHTP, m_Quit;
    public Vector3 m_MainCameraPos, m_SettingsCameraPos;
    public GameObject m_settings, m_htpCanvas;
    public Slider fxVolume, musicVolume;
    AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
        // Bind buttons to functions
        m_StartGame.onClick.AddListener(StartGame);
        m_OpenSettings.onClick.AddListener(OpenSettings);
        m_ReturnFromSettings.onClick.AddListener(Return);
        m_Quit.onClick.AddListener(Quit);
        m_HowToPlay.onClick.AddListener(HowToPlay);
        m_ReturnFromHTP.onClick.AddListener(Return);

        fxVolume.value = PlayerPrefs.GetFloat("fxVolume", 0.6f);
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume", 0.4f);
        fxVolume.onValueChanged.AddListener(FxVolumeChanged);
        musicVolume.onValueChanged.AddListener(MusicVolumeChanged);

        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        bgm.volume = PlayerPrefs.GetFloat("musicVolume") / 2;
    }

    void FxVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("fxVolume", value);
    }

    void MusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
        bgm.volume = value / 2;
    }

    void Update()
    {
        // Ease in/out camera transition between positions
        // m_Camera.transform.position = Vector3.Lerp(m_Camera.transform.position, positions[currentMenu], Time.deltaTime * 3);
    }

    void StartGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    void OpenSettings()
    {
        m_settings.SetActive(true);
    }

    void HowToPlay()
    {
        m_htpCanvas.SetActive(true);
    }

    void Return()
    {
        m_settings.SetActive(false);
        m_htpCanvas.SetActive(false);
    }
    
    void Quit()
    {
        Application.Quit();
    }
}
