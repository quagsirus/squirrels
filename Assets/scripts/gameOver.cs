using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    [SerializeField] [HideInInspector] Squirrel squirrel1;
    [SerializeField] [HideInInspector] Squirrel squirrel2;

    public GameObject gameoverUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        squirrel1 = GameObject.Find("playerOne").GetComponent<Squirrel>();
        squirrel2 = GameObject.Find("playerTwo").GetComponent<Squirrel>();
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
}
