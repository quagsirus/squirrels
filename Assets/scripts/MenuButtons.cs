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
    private int currentMenu;
    const int MAIN = 0;
    const int SETTINGS = 1;
    private Vector3[] positions;

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
        positions = new[] { m_MainCameraPos, m_SettingsCameraPos };
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
        currentMenu = SETTINGS;
        m_settings.SetActive(true);
    }

    void HowToPlay()
    {
        m_htpCanvas.SetActive(true);
    }

    void Return()
    {
        currentMenu = MAIN;
        m_settings.SetActive(false);
        m_htpCanvas.SetActive(false);
    }
    
    void Quit()
    {
        Application.Quit();
    }
}
