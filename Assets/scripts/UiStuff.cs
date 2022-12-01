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
    FxPlayer fxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        p2Lives = GameObject.Find("p2 lives");
        p1Lives = GameObject.Find("p1 lives");
        fxPlayer = GameObject.Find("SFX").GetComponent<FxPlayer>();
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
}
