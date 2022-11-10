using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiStuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    public void SetStat(string statistic, int amount)
    {
        TextMeshProUGUI statText = transform.Find(statistic).GetComponent<TextMeshProUGUI>();
        statText.text = char.ToUpper(statistic[0]) + statistic.Substring(1) + ": " + amount;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
