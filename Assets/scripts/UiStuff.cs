using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiStuff : MonoBehaviour
{
    public Image spriteRenderer;
    public Sprite deadHeart;
    public Sprite aliveHeart;

    GameObject p1Lives, p2Lives, section;

    // Start is called before the first frame update
    void Start()
    {
        p1Lives = GameObject.Find("p1 lives");
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
            case 3:
                break;
            case 2:
                section.transform.Find("heart3").GetComponent<Image>().sprite = deadHeart;
                break;
            case 1:
                section.transform.Find("heart2").GetComponent<Image>().sprite = deadHeart;
                section.transform.Find("heart3").GetComponent<Image>().sprite = deadHeart;
                break;
            case 0:
                section.transform.Find("heart3").GetComponent<Image>().sprite = deadHeart;
                section.transform.Find("heart2").GetComponent<Image>().sprite = deadHeart;
                section.transform.Find("heart1").GetComponent<Image>().sprite = deadHeart;
                break;
        }
    }
}
