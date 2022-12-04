using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiStuff : MonoBehaviour
{
    public Sprite deadHeart;
    public Sprite aliveHeart;

    GameObject p1Lives, p2Lives, section;
    TextMeshProUGUI scoreDisplay;
    FxPlayer fxPlayer;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        p2Lives = GameObject.Find("p2 lives");
        p1Lives = GameObject.Find("p1 lives");
        scoreDisplay = GameObject.Find("score").GetComponent<TextMeshProUGUI>();
        fxPlayer = GameObject.Find("SFX").GetComponent<FxPlayer>();

        AddToScore(score, false);
    }
    public void SetStat(string statistic, int amount)
    {
        TextMeshProUGUI statText = transform.Find(statistic).GetComponent<TextMeshProUGUI>();
        statText.text = char.ToUpper(statistic[0]) + statistic.Substring(1) + ": " + amount;

        if (statistic == "p1 Lives")
        {
            section = p1Lives;
        }
        else if (statistic == "p2 Lives")
        {
            section = p2Lives;
        }
        switch(amount)
        {
            case <=0:
                section.transform.Find("heart1").GetComponent<Image>().sprite = deadHeart;
                section.transform.Find("heart2").GetComponent<Image>().sprite = deadHeart;
                section.transform.Find("heart3").GetComponent<Image>().sprite = deadHeart;
                fxPlayer.PlayDie();
                break;
            case 1:
                section.transform.Find("heart2").GetComponent<Image>().sprite = deadHeart;
                goto case 2;
            case 2:
                section.transform.Find("heart3").GetComponent<Image>().sprite = deadHeart;
                fxPlayer.PlayHit();
                break;
        }
    }
    public void AddToScore(int points, bool isAdding = true)
    {
        if (isAdding)
        {
            score += points;
        } else
        {
            score = points;
        }

        string scoreText = new("");
        string scoreOrig = score.ToString();

        for (int index = 0; index < scoreOrig.Length; index++)
        {
            scoreText += "<sprite index=" + scoreOrig[index] + "> ";
        }
        scoreDisplay.text = scoreText;
    }
}
