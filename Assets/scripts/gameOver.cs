using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    [SerializeField] [HideInInspector] Squirrel squirrel1;
    [SerializeField] [HideInInspector] Squirrel squirrel2;

    public GameObject gameoverUI;
    TMP_InputField textbox;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        squirrel1 = GameObject.Find("playerOne").GetComponent<Squirrel>();
        squirrel2 = GameObject.Find("playerTwo").GetComponent<Squirrel>();
        GameObject.Find("SaveScore").GetComponent<Button>().onClick.AddListener(SubmitToLeaderboard);
        textbox = GameObject.Find("LeaderboardName").GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (squirrel1.isDead && squirrel2.isDead)
        {
            GameOver();
        }
        else if (squirrel1.isDead && squirrel2.isDespawned)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameoverUI.SetActive(true);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    void SubmitToLeaderboard()
    {
        Debug.Log(textbox.text);
    }
}
