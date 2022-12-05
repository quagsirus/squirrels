using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOver : MonoBehaviour
{
    [SerializeField] [HideInInspector] Squirrel squirrel1;
    [SerializeField] [HideInInspector] Squirrel squirrel2;

    public Sprite sadface, success;
    public GameObject gameoverUI;
    UiStuff ui;
    TMP_InputField textbox;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        squirrel1 = GameObject.Find("playerOne").GetComponent<Squirrel>();
        squirrel2 = GameObject.Find("playerTwo").GetComponent<Squirrel>();
        GameObject.Find("SaveScore").GetComponent<Button>().onClick.AddListener(Clicked);
        textbox = GameObject.Find("LeaderboardName").GetComponent<TMP_InputField>();
        ui = GameObject.Find("uiOverlay").GetComponent<UiStuff>();
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
        gameoverUI.transform.localScale = new Vector3(1,1,1);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    void Clicked()
    {
        StartCoroutine(SubmitToLeaderboard());
    }

    IEnumerator SubmitToLeaderboard()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", textbox.text);
        form.AddField("score", ui.score);
        GameObject.Find("SaveScore").GetComponent<Button>().interactable = false;
        textbox.readOnly = true;

        using (UnityWebRequest www = UnityWebRequest.Post("https://squirrels.catpowered.net/add", form))
        {
            www.timeout = 5;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                GameObject.Find("SaveScore").GetComponent<Image>().sprite = sadface;
                GameObject.Find("SaveScore").GetComponent<Button>().interactable = true;
                textbox.readOnly = false;
            }
            else
            {
                Debug.Log("Form upload complete!");
                GameObject.Find("SaveScore").GetComponent<Image>().sprite = success;
            }
        }
    }
}
